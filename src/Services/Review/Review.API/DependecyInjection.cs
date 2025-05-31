using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Blocks.Core;
using Blocks.AspNetCore;
using Blocks.EntityFrameworkCore;
using Blocks.Messaging;
using Review.Persistence.Repositories;
using Carter;

namespace Review.API;

public static class DependecyInjection
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.ConfigureOptionsFromSection<FileStorage.Contracts.FileServerOptions>(configuration)
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
						.AddCarter()
						.AddHttpContextAccessor()                // For accessing HTTP context
						.AddEndpointsApiExplorer()               // Minimal API docs (Swagger)
						.AddSwaggerGen()                         // Swagger setup
						.AddJwtAuthentication(configuration)     // JWT Authentication
						.AddAuthorization();                     // Authorization configuration

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

				return services;
		}
}
