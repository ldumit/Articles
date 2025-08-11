using Articles.Security;
using Blocks.Mapster;
using Blocks.MediatR.Behaviours;
using Blocks.Messaging.MassTransit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Features.Invitations.InviteReviewer;
using Review.Application.StateMachines;
using System.Reflection;

namespace Review.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				//talk - fluid vs normal
				services
						.AddMapsterConfigsFromCurrentAssembly()																	// Scanning for mapping registration
						//.AddMapsterConfigsFromAssemblyContaining<ApplicationMappingConfig>()    // Scanning for mapping registration
						.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()		// Register Fluent validators as transient
						.AddMediatR(config =>
						{
								config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

								config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
								config.AddOpenBehavior(typeof(ValidationBehavior<,>));
								config.AddOpenBehavior(typeof(LoggingBehavior<,>));
						})
						.AddMassTransitWithRabbitMQ(configuration, Assembly.GetExecutingAssembly()); ;
				
				services.AddScoped<IArticleRoleVerifier, ArticleRoleVerifier>();

				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						//var dbConntext = provider.GetRequiredService<SubmissionDbContext>();
						var cache = provider.GetRequiredService<IMemoryCache>();
						return new ArticleStateMachine(articleStage, cache);
				});

				return services;
    }
}
