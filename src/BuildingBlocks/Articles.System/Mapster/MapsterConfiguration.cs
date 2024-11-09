using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Articles.Mapster;
//todo - do I need this global configuration? if the mapping configuration is in the Application project, then GetExecutingAssembly is not good ????
public static class MapsterConfiguration
{
		public static IServiceCollection AddMapster(this IServiceCollection services)
		{
				TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()!);
				return services;
		}
}
