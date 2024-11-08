using Articles.Entitities;
using Articles.System;
using Articles.System.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Articles.EntityFrameworkCore;

public abstract class CachedRepositoryBase<TDbContext, TEntity, TId>(TDbContext _dbContext, IMemoryCache _cache)
		where TDbContext : DbContext
		where TEntity : class, IEntity<TId>, ICacheable 
		where TId : struct
{
		public IEnumerable<TEntity> GetAll()
				=> _cache.GetOrCreate(entry => _dbContext.Set<TEntity>().AsNoTracking().ToList());

		public TEntity GetById(TId id)
				=> GetAll().Single(e => e.Id.Equals(id));
}