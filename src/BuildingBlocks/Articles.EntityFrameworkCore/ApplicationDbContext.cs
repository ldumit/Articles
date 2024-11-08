using Articles.Entitities;
using Articles.System;
using Articles.System.Cache;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Articles.EntityFrameworkCore;

public class ApplicationDbContext<TDbContext>(DbContextOptions<TDbContext> _options, IMemoryCache _cache) : DbContext(_options)
		where TDbContext : DbContext
{
		public virtual IEnumerable<TEntity> GetAllCached<TEntity>()
				where TEntity : class, ICacheable
				=> _cache.GetOrCreate(entry => this.Set<TEntity>().AsNoTracking().ToList());

		public virtual TEntity GetByIdCached<TEntity, TId>(TId id)
				where TEntity : class, IEntity<TId>, ICacheable
				where TId : struct
				=> GetAllCached<TEntity>().Single(e => e.Id.Equals(id));

		public virtual IEnumerable<TDestination> GetAllCached<TEntity, TDestination>()
				where TEntity : class, ICacheable
				=> _cache.GetOrCreate(entry => this.Set<TEntity>().AsNoTracking().ProjectToType<TDestination>().ToList());
}
