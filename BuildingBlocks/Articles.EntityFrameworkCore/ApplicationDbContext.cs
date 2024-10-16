using Articles.System;
using Articles.System.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Articles.EntityFrameworkCore;

public class ApplicationDbContext<TDbContext>(DbContextOptions<TDbContext> _options, IMemoryCache _cache) : DbContext(_options)
		where TDbContext : DbContext
{
		public virtual IEnumerable<TEntity> GetCached<TEntity>()
				where TEntity : class, ICacheable
				=> _cache.GetOrCreate(entry => this.Set<TEntity>().ToList());
}
