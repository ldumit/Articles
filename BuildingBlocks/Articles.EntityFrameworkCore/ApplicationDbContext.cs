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

		public async Task<int> TrySaveChangesAsync(CancellationToken cancellationToken = default)
		{
				try
				{
						return await SaveChangesImpl(cancellationToken);
				}
				catch (Exception ex)
				{
						//Log.Error(ex, ex.Message);
						return -1;
				}
		}

		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
				if (base.Database.CurrentTransaction == null)
				{
						using var transaction = base.Database.BeginTransaction();
						var counter = await SaveChangesAndDispatchEventsAsync(cancellationToken);
						transaction.Commit();

						return counter;
				}
				else
				{
						return await SaveChangesAndDispatchEventsAsync(cancellationToken);
				}

		}

		private async Task<int> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
		{
				// save first the main changes
				int counter = await SaveChangesImpl(cancellationToken);

				//todo implement events dispatching with fastendpoints
				int dispatchedEventsCounter = 0;
				//int dispatchedEventsCounter = (await _mediator.DispatchDomainEventsAsync(this));
				if (dispatchedEventsCounter > 0)
						// save changes from event handlers
						// todo domain events handlers should save their own changes
						await TrySaveChangesAsync(cancellationToken);

				return counter;
		}

		private async Task<int> SaveChangesImpl(CancellationToken cancellationToken = default)
		{
				return await base.SaveChangesAsync(cancellationToken);
		}
}
