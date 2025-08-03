using Auth.Grpc;
using Blocks.AspNetCore;
using Blocks.AspNetCore.Grpc;
using Blocks.Core;
using Blocks.Messaging;
using EmailService.Smtp;
using EmailService.Empty;
using FileStorage.MongoGridFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Review.API.FileStorage;
using System.Text.Json.Serialization;
using TransactionOptions = Blocks.EntityFrameworkCore.TransactionOptions;

namespace Review.API;

public static class DependecyInjection
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddAndValidateOptions<RabbitMqOptions>(configuration)
						.AddAndValidateOptions<TransactionOptions>(configuration)
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
		{
				services
						.AddMemoryCache()                       // Basic Caching 
						.AddCarter()														// Register Minimal Api endpoints		
						.AddHttpContextAccessor()								// For accessing HTTP context
						.AddEndpointsApiExplorer()              // Minimal API docs (Swagger)
						.AddSwaggerGen()                        // Swagger setup
						.AddJwtAuthentication(config)    // JWT Authentication
						.AddAuthorization();                    // Authorization configuration

				// http
				// talk - interface segragation
				services
						.AddScoped<IClaimsProvider, HttpContextProvider>()
						.AddScoped<IRouteProvider, HttpContextProvider>()
						.AddScoped<HttpContextProvider>();

				// authorization
				services.AddScoped<IAuthorizationHandler, ArticleAccessAuthorizationHandler>();
						
				// external services or modules
				services.AddMongoFileStorageAsSingletone(config);
				services.AddMongoFileStorageAsScoped<SubmissionFileStorageOptions>(config);
				services.AddFileServiceFactory();

				services.AddEmptyEmailService(config); //todo replace it with a real implementation
				//services.AddSmtpEmailService(config);

				// grpc Services
				var grpcOptions = config.GetSectionByTypeName<GrpcServicesOptions>();
				services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");
				// todo - add this service
				//services.AddConfiguredGrpcClient<JournalService.JournalerviceClient>(grpcOptions);

				//todo do I need this IThreadSafeMemoryCache?
				//services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();

				return services;
		}
}
