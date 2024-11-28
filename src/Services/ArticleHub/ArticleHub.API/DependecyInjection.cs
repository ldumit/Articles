using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Blocks.Core;
using Blocks.Messaging;
using Articles.Security;
using ArticleHub.Persistence;

namespace ArticleHub.API;

public static class DependecyInjection
{
		public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.ConfigureOptions<RabbitMqOptions>(configuration)
						.ConfigureOptions<HasuraOptions>(configuration)
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddHttpContextAccessor()                // For accessing HTTP context
						.AddEndpointsApiExplorer()               // Minimal API docs (Swagger)
						.AddSwaggerGen()                         // Swagger setup
						.AddJwtAuthentication(configuration)     // JWT Authentication
						.AddAuthorization();                     // Authorization configuration

				//// http
				//services
				//		.AddScoped<IClaimsProvider, HttpContextProvider>()
				//		.AddScoped<IRouteProvider, HttpContextProvider>()
				//		.AddScoped<HttpContextProvider>();

				//// authorization
				//services
				//		.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>()
				//		.AddScoped<IArticleRoleChecker, ContributorRepository>();

				return services;
		}
}
