using Blocks.EntityFrameworkCore.Interceptors;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Review.Persistence;

public static class DependencyInjectionG
{
    public static IServiceCollection AddPersistenceServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

				services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

				// decide if we need the same DBConnection/Transaction when we are saving the Timeline
				services.AddScoped<DbConnection>(provider =>
				{
						return new SqlConnection(connectionString);
				});
				services.AddDbContext<ReviewDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
						options.UseSqlServer(dbConnection);
				});
				
				services.AddScoped<TransactionProvider>();

				services.AddScoped(typeof(Repository<>));
				services.AddConcreteImplementationsOfGeneric(typeof(Repository<>)); // inherits from Repository<>
				services.AddScoped<AssetTypeRepository>();


				//services.AddScoped<ArticleRepository>();
				//services.AddScoped<AssetRepository>();
				//services.AddScoped<AssetTypeRepository>();
				//services.AddScoped<PersonRepository>();
				services.AddHostedService<DatabaseCacheLoader>();

				return services;
    }
}
