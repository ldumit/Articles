using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Blocks.Core;
using Blocks.EntityFrameworkCore;
using System.Reflection;
using Production.Application;
using FastEndpoints.Swagger;
using Blocks.AspNetCore;
using FileStorage.AzureBlob;
using Production.Persistence.Repositories;

namespace Production.API;

public static class DependecyInjection
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
				//.AddAndValidateOptions<FileStorage.Contracts.FileServerOptions>(configuration)
				.AddAndValidateOptions<TransactionOptions>(configuration)
				.Configure<JsonOptions>(opt =>
				{
						opt.SerializerOptions.PropertyNameCaseInsensitive = true;
						opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
		{
				//talk - fluid vs normal
				services.AddControllers();

				services
						.AddMemoryCache()
						.AddFastEndpoints()
						.AddMapster()
						.SwaggerDocument()
						.AddEndpointsApiExplorer()
						.AddAutoMapper(new Assembly[] { typeof(Production.API.Features.Shared.FileResponseMappingProfile).Assembly })
						.AddDistributedMemoryCache() //.AddMemoryCache()
						.AddSwaggerGen()
						.AddJwtAuthentication(configuration)
						.AddAuthorization();


				//talk - SOLID principle interface segragation, injecting multiple interfaces using the same class
				services.AddScoped<HttpContextProvider>();
				services.AddScoped<IClaimsProvider, HttpContextProvider>();
				services.AddScoped<IRouteProvider, HttpContextProvider>();

				services.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>();
				services.AddScoped<IArticleRoleChecker, ContributorRepository>();

				// monolith modules registration 
				services.AddAzureFileStorage(configuration);

				return services;
		}
}