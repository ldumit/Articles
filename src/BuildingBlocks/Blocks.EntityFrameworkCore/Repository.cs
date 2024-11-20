using Blocks.Entitities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Blocks.Exceptions;

namespace Blocks.EntityFrameworkCore;

public interface IRepository<Entity> : IRepository<Entity, int>;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity> GetByIdAsync(TKey id, bool throwNotFound = true);
    IQueryable<TDto> GetAll<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy);
    Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    Task<bool> DeleteByIdAsync(TKey id);
    void Remove(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void RemoveRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void ClearTracking();

    //bool SoftDelete(TKey id);
}

public class Repository<TContext, TEntity>(TContext dbContext) : RepositoryBase<TContext, TEntity, int>(dbContext)
		where TContext : DbContext
		where TEntity : class, IEntity<int>;

public abstract class RepositoryBase<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
		where TContext : DbContext
		where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly TContext _dbContext;
    protected readonly DbSet<TEntity> _entity;

		public RepositoryBase(TContext dbContext)
    {
        _dbContext = dbContext;
        _entity = _dbContext.Set<TEntity>();
		}
    public TContext Context => _dbContext;
		public virtual DbSet<TEntity> Entity => _entity;

    protected virtual IQueryable<TEntity> Query() => _entity;

    public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

		public virtual async Task<TEntity> FindByIdAsync(TKey id, bool throwNotFound = false)
		{
				var entity = await _entity.FindAsync(id);
				return ReturnOrThrow(entity, throwNotFound);
		}

		public virtual async Task<TEntity> GetByIdAsync(TKey id, bool throwNotFound = true)
		{
				var entity = await Query().SingleOrDefaultAsync(e => e.Id.Equals(id));
				return ReturnOrThrow(entity, throwNotFound);
		}

		protected TEntity ReturnOrThrow(TEntity? entity, bool throwNotFound)
		{
				if (throwNotFound && entity is null)
						throw new NotFoundException(NotFoundMessage);
				return entity!;
		}

		public virtual string NotFoundMessage => $"{typeof(TEntity).Name} not found";

		public virtual async Task<TEntity> GetByIdWithIncludesAsync(
				TKey id, bool throwNotFound = false,
				params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] includeProperties)
		{
				IQueryable<TEntity> query = Query();

				foreach (var include in includeProperties)
				{
						query = include.Compile()(query);
				}

				var entity = await query.SingleOrDefaultAsync(e => e.Id.Equals(id));
				return ReturnOrThrow(entity, throwNotFound);
		}

		public virtual async Task<bool> ExistsAsync(TKey id)
		{
				return await Entity.AnyAsync(e => e.Id.Equals(id));
		}

    //public TEntity? GetOrDefault(TKey id) => Query().SingleOrDefault(e => e.Id == id);

    public IQueryable<TDto> GetAll<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy)
    {
        var items = _entity.AsNoTracking().OrderBy(orderBy);

        //var expression = new ProjectionExpressionOptimiser().Optimise(projection);

        return items.Select(projection);
    }
    public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy)
    {
        return await GetAll(projection, orderBy).ToListAsync();
    }

    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) { return _entity.Where(predicate); }
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate) { return _entity.Where(predicate); }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await _entity.AddAsync(entity);
        return addedEntity.Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return _entity.Update(entity).Entity;
    }

		public virtual void Remove(TEntity entity)
		{
				_entity.Remove(entity);
		}

		public virtual async Task<bool> DeleteByIdAsync(TKey id)
		{
        // talk - executing an SQL is the most eficient way
        // talk - setting the state to Deleted doesn't work
        // because it requires to instantiate an empty entity first which is not possible if the entity has required properties
				//		var entity = new TEntity { Id = id };
				//		_dbContext.Entry(entity).State = EntityState.Deleted;
				var rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
				return rowsAffected > 0;
		}


		public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _entity.AddRangeAsync(entities);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _entity.RemoveRange(entities);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _entity.UpdateRange(entities);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
				=> await _dbContext.SaveChangesAsync(cancellationToken);

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }
    public void ClearTracking()
    {
        _dbContext.ChangeTracker.Clear();
    }

		//public bool SoftDelete(TKey id)
		//{

		//    TEntity entity = Get(id);

		//    if (entity != null && typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
		//    {

		//        typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
		//        var updated = _context.Set<TEntity>().Update(entity).Entity;
		//        return true;
		//    }

		//    return false;
		//}
}