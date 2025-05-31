using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Blocks.Core;
using Blocks.AspNetCore;
using Blocks.EntityFrameworkCore;
using Blocks.Messaging;
using Submission.Persistence.Repositories;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using FileStorage.MongoGridFS;

namespace Submission.API;

public static class DependecyInjection
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						//.ConfigureOptions<FileStorage.Contracts.FileServerOptions>(configuration)
						.ConfigureOptionsFromSection<RabbitMqOptions>(configuration)
						.ConfigureOptionsFromSection<TransactionOptions>(configuration)
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddMemoryCache()														// Basic Caching 
						.AddHttpContextAccessor()										// For accessing HTTP context
						.AddEndpointsApiExplorer()									// Minimal API docs (Swagger)
						.AddSwaggerGen()														// Swagger setup
						.AddJwtAuthentication(configuration)				// JWT Authentication
						.AddAuthorization();												// Authorization configuration

				// http
				// talk - interface segragation
				services
						.AddScoped<IClaimsProvider, HttpContextProvider>()
						.AddScoped<IRouteProvider, HttpContextProvider>()
						.AddScoped<HttpContextProvider>();

				// authorization
				services
						.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>()
						.AddScoped<IArticleRoleChecker, ContributorRepository>();

				//file storage
				services.AddMongoFileStorage(configuration);
				//services.AddAzureFileStorage(configuration);


				//grpc Services
				var grpcOptions = configuration.GetSectionByTypeName<GrpcServicesOptions>();
				services.AddConfiguredGrpcClient<AuthService.AuthServiceClient>(grpcOptions);
				//services.AddConfiguredGrpcClient<JournalService.JournalerviceClient>(grpcOptions);

				return services;
		}
}
