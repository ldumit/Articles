using Articles.Security;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Production.Application.StateMachines;
using Production.Persistence;
using Production.Persistence.Repositories;
using System.Reflection;

namespace Production.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				services.AddMediatR(config =>
				{
						config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
						//config.AddOpenBehavior(typeof(ValidationBehavior<,>));
						//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
				});

				//services.AddFeatureManagement();
				//services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

				//services.AddScoped<IValidator<AssignTypesetterCommand>, AssignTypesetterCommandValidator>();

				//services.AddScoped<IEventHandler<ArticleStageChangedDomainEvent>, AddTimelineWhenArticleStageChangedEventHandler>();
				//services.AddEventHandlersFromAssembly(typeof(AddTimelineWhenArticleStageChangedEventHandler).Assembly);			


				//overides the singleton lifetime for validators done by FastEndpoints
				//builder.Services.Scan(scan => scan
				//		.FromAssemblyOf<Program>()
				//		.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
				//		.AsImplementedInterfaces()
				//		.WithScopedLifetime());

				//services.AddScoped<ArticleStateMachine>(); 


				services.AddScoped<IArticleRoleVerifier, ArticleRoleVerifier>();

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

		public static IServiceCollection AddMapster(this IServiceCollection services)
		{
				TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);
				return services;
		}
}
