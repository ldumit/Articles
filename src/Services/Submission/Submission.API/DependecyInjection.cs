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
using Journals.Grpc;

namespace Submission.API;

public static class DependecyInjection
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration config)
		{
				services
						.AddAndValidateOptions<RabbitMqOptions>(config)
						.AddAndValidateOptions<TransactionOptions>(config)
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
		{
				services
						.AddMemoryCache()														// Basic Caching 
						.AddHttpContextAccessor()										// For accessing HTTP context
						.AddEndpointsApiExplorer()									// Minimal API docs (Swagger)
						.AddSwaggerGen()														// Swagger setup
						.AddJwtAuthentication(config)				// JWT Authentication
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

				// external services or modules
				services.AddMongoFileStorage(config);

				//grpc Services
				var grpcOptions = config.GetSectionByTypeName<GrpcServicesOptions>();
				services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
				services.AddCodeFirstGrpcClient<IJournalService>(grpcOptions, "Journal");


				return services;
		}
}
