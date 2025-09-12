using Articles.Security;
using Blocks.Domain;
using Blocks.Mapster;
using Blocks.MediatR.Behaviours;
using Blocks.Messaging.MassTransit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using Submission.Application.StateMachines;
using System.Reflection;

namespace Submission.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				//todo - remove all commented code after testing Submission
				//talk - fluid vs normal
				services
						.AddMapsterConfigsFromCurrentAssembly()																	// Scanning for mapping registration
						//.AddMapsterConfigsFromAssemblyContaining<ApplicationMappingConfig>()    // Scanning for mapping registration
						.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()		// Register Fluent validators as transient
						.AddMediatR(config =>
						{
								config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

								config.AddOpenBehavior(typeof(AssignUserIdBehavior<,>));
								config.AddOpenBehavior(typeof(ValidationBehavior<,>));
								config.AddOpenBehavior(typeof(LoggingBehavior<,>));
						})
						.AddMassTransitWithRabbitMQ(configuration, Assembly.GetExecutingAssembly());

				services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
				services.AddScoped<IArticleRoleVerifier, ArticleRoleVerifier>();

				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						var cache = provider.GetRequiredService<IMemoryCache>();
						return new ArticleStateMachine(articleStage, cache);
				});

				return services;
    }
}
