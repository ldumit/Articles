using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blocks.Mapster;
public static class MapsterConfiguration
{
		public static IServiceCollection AddMapster(this IServiceCollection services, Assembly? assembly = null)
		{
				if (assembly is null)
						assembly = Assembly.GetExecutingAssembly()!;
				
				TypeAdapterConfig.GlobalSettings.Scan(assembly);
				return services;
		}
		public static IServiceCollection AddMapsterFromAssemblyContaining<T>(this IServiceCollection services)
		{
				TypeAdapterConfig.GlobalSettings.Scan(typeof(T).Assembly);
				return services;
		}
}
