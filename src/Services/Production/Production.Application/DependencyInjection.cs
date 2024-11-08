using Articles.AspNetCore;
using Articles.EntityFrameworkCore;
using Articles.Security;
using Articles.System;
using ArticleTimeline.Application.EventHandlers;
using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Persistence;
using FastEndpoints;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Production.Application.StateMachines;
using Production.Domain.Events;
using Production.Persistence;
using Production.Persistence.Repositories;
using System.Data.Common;
using System.Reflection;

namespace Production.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

				services.AddMediatR(config =>
				{
						config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
						//config.AddOpenBehavior(typeof(ValidationBehavior<,>));
						//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
				});

				//services.AddFeatureManagement();
				//services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());


				services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

				// decide if we need the same DBConnection/Transaction when we are saving the Timeline
				services.AddScoped<DbConnection>(provider =>
				{
						return new SqlConnection(connectionString);
				});
				services.AddDbContext<ProductionDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
						options.UseSqlServer(dbConnection);
				});
				
				services.AddScoped<TransactionProvider>();
				//services.AddDbContext<ArticleTimelineDbContext>((provider, options) =>
				//{
				//		var dbConnection = provider.GetRequiredService<DbConnection>();
				//		//options.UseSqlServer(dbConnection);
				//		options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
				//		options.UseSqlServer(dbConnection, options =>
				//		{
				//				options.MigrationsHistoryTable("__EFMigrationsHistory", "ArticleTimeline");
				//		});
				//});

				services.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>();

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

				//services.AddScoped<IEventHandler<ArticleStageChangedDomainEvent>, AddTimelineWhenArticleStageChangedEventHandler>();
				//services.AddEventHandlersFromAssembly(typeof(AddTimelineWhenArticleStageChangedEventHandler).Assembly);			

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
