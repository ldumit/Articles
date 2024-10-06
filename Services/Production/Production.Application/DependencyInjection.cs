using Articles.AspNetCore;
using Articles.Security;
using Articles.System;
using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Persistence;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Production.Application.StateMachines;
using Production.Persistence;
using Production.Persistence.Repositories;
using System.Data.Common;

namespace Production.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

				//services.AddMediatR(config =>
				//{
				//    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				//    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
				//    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
				//});

				//services.AddFeatureManagement();
				//services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());


				// decide if we need the same DBConnection/Transaction when we are saving the Timeline
				services.AddScoped<DbConnection>(provider =>
				{
						return new SqlConnection(connectionString);
				});
				services.AddDbContext<ProductionDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						//options.UseSqlServer(dbConnection);
						options.UseSqlServer(connectionString);

				});
				services.AddDbContext<ArticleTimelineDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						options.UseSqlServer(dbConnection);
				});

				services.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>();

				services.AddScoped<ClaimsProvider>();

        //talk - SOLID principle interface segragation, injecting multiple interfaces using the same class
				services.AddScoped<IClaimsProvider, HttpContextProvider>(); 
        services.AddScoped<IRouteProvider, HttpContextProvider>();
        services.AddScoped<HttpContextProvider>();

        services.AddScoped<IArticleRoleChecker, ActorRepository>();
				services.AddScoped<ArticleRepository>();
				services.AddScoped<AssetRepository>();
				services.AddScoped<FileRepository>();
				services.AddScoped<PersonRepository>();

				services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();
				services.AddScoped<IFileService, FileService>();
				services.AddArticleTimelineVariableResolvers();

				//services.AddScoped<ArticleStateMachine>(); 
				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						var dbConntext = provider.GetRequiredService<ProductionDbContext>();
						return new ArticleStateMachine(articleStage, dbConntext);
				});
				
				//services.AddScoped<AssetStateMachine>();
				services.AddScoped<AssetStateMachineFactory>(provider => assetState =>
				{
						var dbConntext = provider.GetRequiredService<ProductionDbContext>();
						return new AssetStateMachine(assetState, dbConntext);
				});

				return services;
    }
}
