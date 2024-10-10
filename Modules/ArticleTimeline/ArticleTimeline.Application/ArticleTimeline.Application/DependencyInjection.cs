using ArticleTimeline.Persistence;
using ArticleTimeline.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace ArticleTimeline.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddTimelineApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				services.AddDbContext<ArticleTimelineDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						//options.UseSqlServer(dbConnection);
						options.UseSqlServer(dbConnection, options =>
						{
								options.MigrationsHistoryTable("__EFMigrationsHistory", "ArticleTimeline");
						});
				});

				services.AddScoped<TimelineRepository>();


				return services;
    }
}
