using ArticleTimeline.Persistence;
using ArticleTimeline.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Reflection;

namespace ArticleTimeline.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddTimelineApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				services.AddDbContext<ArticleTimelineDbContext>((provider, options) =>
				{
						//options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
						var dbConnection = provider.GetRequiredService<DbConnection>();
						
						options.UseSqlServer(dbConnection, options =>
						{
								options.MigrationsHistoryTable("__EFMigrationsHistory", "ArticleTimeline");
						});
				});

				services.AddMediatR(config =>
				{
						config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				});
				services.AddScoped<TimelineRepository>();


				return services;
    }
}
