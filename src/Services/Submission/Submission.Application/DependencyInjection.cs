using Articles.AspNetCore;
using Articles.EntityFrameworkCore;
using Articles.MediatR.Behaviours;
using Articles.Security;
using Articles.System;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using Submission.Application.StateMachines;
using Submission.Domain.StateMachines;
using Submission.Persistence;
using Submission.Persistence.Repositories;
using System.Data.Common;
using System.Reflection;

namespace Submission.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

				services.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>(); // Register all validators as transient
				services.AddMediatR(config =>
				{
						config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

						config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
						config.AddOpenBehavior(typeof(ValidationBehavior<,>));
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
				services.AddDbContext<SubmissionDbContext>((provider, options) =>
				{
						var dbConnection = provider.GetRequiredService<DbConnection>();
						options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
						options.UseSqlServer(dbConnection);
				});
				
				services.AddScoped<TransactionProvider>();

				#region Authorization
				services.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>();
				services.AddScoped<IArticleRoleChecker, ActorRepository>();

				services.AddScoped<IClaimsProvider, HttpContextProvider>();
				services.AddScoped<IRouteProvider, HttpContextProvider>();
				services.AddScoped<HttpContextProvider>();

				#endregion
				services.AddScoped(typeof(CachedRepository<,>));
				services.AddScoped(typeof(Repository<>));
				services.AddScoped<ArticleRepository>();
				services.AddScoped<AssetRepository>();
				services.AddScoped<PersonRepository>();

				services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();
				services.AddScoped<IFileService, FileService>();
				//services.AddArticleTimelineVariableResolvers();

				//services.AddScoped<IEventHandler<ArticleStageChangedDomainEvent>, AddTimelineWhenArticleStageChangedEventHandler>();
				//services.AddEventHandlersFromAssembly(typeof(AddTimelineWhenArticleStageChangedEventHandler).Assembly);			

				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						var dbConntext = provider.GetRequiredService<SubmissionDbContext>();
						return new ArticleStateMachine(articleStage, dbConntext);
				});

				return services;
    }
		public static IServiceCollection AddMapster(this IServiceCollection services)
		{
				TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()!);

				return services;
		}
}
