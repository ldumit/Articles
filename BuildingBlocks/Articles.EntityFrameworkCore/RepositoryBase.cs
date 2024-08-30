using Articles.Entitities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using Articles.Exceptions;

namespace Articles.EntityFrameworkCore;

public interface IRepository<Entity> : IRepository<Entity, int>
{

}
public interface IRepository<TEntity, TKey>
{
    TEntity GetById(TKey id, bool throwNotFound = true);
    IQueryable<TDto> GetAll<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy);
    Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate);
    bool IsValid(TKey id);
    TEntity Add(TEntity entity);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    bool Delete(TKey id);
    void Delete(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void RemoveRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task<int> SaveChangesAsync();
    void ClearTracking();

    //bool SoftDelete(TKey id);
}

public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, int>
    where TEntity : class, IEntity
{
    public RepositoryBase(DbContext context, IMultitenancy? multitenancy = null) : base(context)
    {
    }
}

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    protected DbSet<TEntity> Entity => _context.Set<TEntity>();

    protected virtual IQueryable<TEntity> Query() => Entity;

    public virtual string NotFoundMessage => $"{nameof(TEntity)} not found";
    public virtual bool Exists(TKey id)
    {
        return Entity.Any(e => e.Id.Equals(id));
    }


    public virtual TEntity GetById(TKey id, bool throwNotFound = true)
    {
        var entity = Entity.Find(id);
        if (throwNotFound == true && entity == null)
            throw new HttpException(HttpStatusCode.NotFound, NotFoundMessage);
        return entity;
    }

    //public TEntity? GetOrDefault(TKey id) => Query().SingleOrDefault(e => e.Id == id);

    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public bool IsValid(TKey id)
    {
        return this.GetById(id) != null;
    }

    public IQueryable<TDto> GetAll<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy)
    {
        var items = _context.Set<TEntity>().AsNoTracking().OrderBy(orderBy);

        //var expression = new ProjectionExpressionOptimiser().Optimise(projection);

        return items.Select(projection);
    }
    public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> projection, Expression<Func<TEntity, object>> orderBy)
    {
        return await GetAll(projection, orderBy).ToListAsync();
    }
    public virtual IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) { return _context.Set<TEntity>().Where(predicate); }
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate) { return _context.Set<TEntity>().Where(predicate); }

    public virtual TEntity Add(TEntity entity)
    {
        return _context.Set<TEntity>().Add(entity).Entity;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await _context.Set<TEntity>().AddAsync(entity);
        return addedEntity.Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return _context.Set<TEntity>().Update(entity).Entity;
    }

    public virtual void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public virtual bool Delete(TKey id)
    {
        TEntity entity = GetById(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            return true;
        }
        return false;
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
    //public Task<int> TrySaveChangesAsync()
    //{
    //    return _context.TrySaveChangesAsync();
    //}

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
    public void ClearTracking()
    {
        _context.ChangeTracker.Clear();
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