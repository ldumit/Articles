using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Review.Persistence;

public class DatabaseCacheLoader(IServiceProvider _serviceProvider) : IHostedService
{
		public Task StartAsync(CancellationToken cancellationToken)
		{
				using (var scope = _serviceProvider.CreateScope())
				{
						var dbContext = scope.ServiceProvider.GetRequiredService<ReviewDbContext>();
						
						dbContext.GetAllCached<ArticleStageTransition>();
				}

				return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
