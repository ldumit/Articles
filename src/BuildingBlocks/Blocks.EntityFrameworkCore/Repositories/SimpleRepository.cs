namespace Blocks.EntityFrameworkCore;

public abstract class SimpleRepository<TContext, TEntity> : ISimpleRepository<TEntity>
		where TContext : DbContext
		where TEntity : class, IEntity
{
		protected readonly TContext _dbContext;
		protected readonly DbSet<TEntity> _entity;

		public SimpleRepository(TContext dbContext)
		{
				_dbContext = dbContext;
				_entity = _dbContext.Set<TEntity>();
		}

		public TContext Context => _dbContext;
		public virtual DbSet<TEntity> Entity => _entity;
		public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

		protected virtual IQueryable<TEntity> Query() => _entity;

		public virtual async Task<TEntity?> FindByIdAsync(int id)
				=> await _entity.FindAsync(id);
		public virtual async Task<TEntity?> GetByIdAsync(int id)
				=> await Query().SingleOrDefaultAsync(e => e.Id.Equals(id));

		public virtual async Task<TEntity> AddAsync(TEntity entity)
				=> (await _entity.AddAsync(entity)).Entity;

		public virtual TEntity Update(TEntity entity)
				=> _entity.Update(entity).Entity;

		public virtual void Remove(TEntity entity)
				=> _entity.Remove(entity);

		public virtual async Task<bool> DeleteByIdAsync(int id)
		{
				var rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
				return rowsAffected > 0;
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
				=> await _dbContext.SaveChangesAsync(cancellationToken);
}
