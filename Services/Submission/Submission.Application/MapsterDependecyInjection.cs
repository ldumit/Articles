using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Submission.Application
{
		public static class MapsterDependecyInjection
		{
				public static IServiceCollection AddMapster(this IServiceCollection services)
				{
						TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);
						//var config = TypeAdapterConfig.GlobalSettings;
						//config.Scan(Assembly.GetAssembly(typeof(Program)));

						//options?.Invoke(config);

						//services.AddSingleton(config);
						//services.AddScoped<IMapper, ServiceMapper>();

						return services;
				}
		}
}
