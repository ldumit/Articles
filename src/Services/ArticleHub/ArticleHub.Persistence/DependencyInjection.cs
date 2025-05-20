using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Blocks.Core;

namespace ArticleHub.Persistence;

public static class DependencyInjection
{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
				var hasuraOptions = configuration.GetSectionByTypeName<HasuraOptions>();

				services.AddDbContext<ArticleHubDbContext>(options 
						=> options.UseNpgsql(configuration.GetConnectionString("Database")));

				services.AddSingleton(_ =>
				{
						var jsonSerializerOptions = new JsonSerializerOptions
						{
								PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
								DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
						};

						var graphQLClientOptions = new GraphQLHttpClientOptions
						{
								EndPoint = new Uri(hasuraOptions.Endpoint)
						};

						var graphQLHttpClient = new GraphQLHttpClient(graphQLClientOptions, new SystemTextJsonSerializer(jsonSerializerOptions));

						//graphQLHttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", hasuraOptions.AdminSecret);
						graphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", hasuraOptions.AdminSecret);

						// add other headers if needed, like tenant id, etc.

						return graphQLHttpClient;
				});

				services.AddScoped<ArticleGraphQLQuery>();
				services.AddScoped<ArticleRepository>();

				return services;
		}
}
