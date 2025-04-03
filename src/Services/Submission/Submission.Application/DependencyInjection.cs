using Blocks.MediatR.Behaviours;
using Blocks.Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using Submission.Application.StateMachines;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Blocks.Messaging.MassTransit;
using Submission.Application.Mappings;

namespace Submission.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				//todo - remove all commented code after testing Submission
				//talk - fluid vs normal
				services
						.AddMapster()																														// Scanning for mapping registration
						.AddMapsterFromAssemblyContaining<ApplicationMappingConfig>()						// Register Mapster mappings
						.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()		// Register Fluent validators as transient
						.AddMediatR(config =>
						{
								config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

								config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
								config.AddOpenBehavior(typeof(ValidationBehavior<,>));
								config.AddOpenBehavior(typeof(LoggingBehavior<,>));
						})
						.AddMassTransit(configuration, Assembly.GetExecutingAssembly()); ;


				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						var cache = provider.GetRequiredService<IMemoryCache>();
						return new ArticleStateMachine(articleStage, cache);
				});

				return services;
    }
}
