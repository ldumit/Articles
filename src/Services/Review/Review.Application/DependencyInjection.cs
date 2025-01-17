using Blocks.MediatR.Behaviours;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using Blocks.Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.StateMachines;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Azure.Storage.Blobs;
using Blocks.Messaging.MassTransit;
using Review.Application.Mappings;
using EmailService.Smtp;
using EmailService.Contracts;
using Review.Application.Features.Invitations.InviteReviewer;

namespace Review.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				//talk - fluid vs normal
				services
						.AddMemoryCache()                       // Basic Caching 
						.AddMapster()                           // Scanning for mapping registration
						.AddMapsterFromAssemblyContaining<ApplicationMappingConfig>()                      // Register Mapster mappings
						.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()		// Register Fluent validators as transient
						.AddMediatR(config =>
						{
								config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

								config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
								config.AddOpenBehavior(typeof(ValidationBehavior<,>));
								//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
						})
						.AddMassTransit(configuration, Assembly.GetExecutingAssembly()); ;
				
				services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("FileServer")));

				//services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();
				services.AddScoped<IFileService, FileService>();
				services.AddScoped<IEmailService, SmtpEmailService>();

				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						//var dbConntext = provider.GetRequiredService<SubmissionDbContext>();
						var cache = provider.GetRequiredService<IMemoryCache>();
						return new ArticleStateMachine(articleStage, cache);
				});

				return services;
    }
}
