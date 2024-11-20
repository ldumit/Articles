using Blocks.Entitities;
using Blocks.Core;
using Blocks.Core.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFrameworkCore;

public abstract class CachedRepository<TDbContext, TEntity, TId>(TDbContext _dbContext, IMemoryCache _cache)
		where TDbContext : DbContext
		where TEntity : class, IEntity<TId>, ICacheable 
		where TId : struct
{
		public IEnumerable<TEntity> GetAll()
				=> _cache.GetOrCreate(entry => _dbContext.Set<TEntity>().AsNoTracking().ToList());

		public TEntity GetById(TId id)
				=> GetAll().Single(e => e.Id.Equals(id));
}