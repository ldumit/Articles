using Articles.Security;
using Auth.Grpc;
using Blocks.AspNetCore.Grpc;
using Blocks.Mapster;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace Journals.API;

public static class DependencyInjection
{
		public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddAndValidateOptions<JwtOptions>(configuration)
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});

				return services;
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
		{
				///services.AddControllers();

				services
					.AddFastEndpoints()
					.AddEndpointsApiExplorer()
					.AddSwaggerGen()
					.AddJwtAuthentication(config)
					.AddMapster()
					.AddAuthorization()
					;

				var grpcOptions = config.GetSectionByTypeName<GrpcServicesOptions>();
				services.AddCodeFirstGrpcClient<IPersonService>(grpcOptions, "Person");


				return services;
		}
}
