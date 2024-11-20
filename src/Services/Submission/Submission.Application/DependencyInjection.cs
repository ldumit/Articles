using Blocks.MediatR.Behaviours;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using FluentValidation;
using Blocks.Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using Submission.Application.StateMachines;
using Submission.Domain.StateMachines;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Submission.Application.Dtos;
using Azure.Storage.Blobs;

namespace Submission.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
				//talk - fluid vs normal
				services
						.AddMemoryCache()                       // Basic Caching 
						.AddMapster()                           // Scanning for mapping registration
						.AddMapsterFromAssemblyContaining<MappingConfig>()                      // Register Mapster mappings
						.AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>()		// Register Fluent validators as transient
						.AddMediatR(config =>
						{
								config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

								config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
								config.AddOpenBehavior(typeof(ValidationBehavior<,>));
								//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
						});
				
				services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("FileServer")));

				//services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();
				services.AddScoped<IFileService, FileService>();

				services.AddScoped<ArticleStateMachineFactory>(provider => articleStage =>
				{
						//var dbConntext = provider.GetRequiredService<SubmissionDbContext>();
						var cache = provider.GetRequiredService<IMemoryCache>();
						return new ArticleStateMachine(articleStage, cache);
				});

				return services;
    }
}
