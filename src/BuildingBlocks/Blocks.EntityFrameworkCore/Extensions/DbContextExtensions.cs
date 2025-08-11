using Blocks.Core.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.EntityFrameworkCore;

public static partial class DbContextExtensions
{
		public static void UnTrackCacheableEntities(this DbContext context)
		{
				foreach (var entry in context.ChangeTracker.Entries())
				{
						if (entry.Entity is ICacheable)
								entry.State = EntityState.Unchanged; // Mark as Unchanged to prevent modifications
				}
		}

		public static WebApplication Migrate<TDbContext>(this WebApplication app)
				where TDbContext : DbContext
		{
				using var scope = app.Services.CreateScope();

				var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

				context.Database.Migrate();
				return app;
		}
}
