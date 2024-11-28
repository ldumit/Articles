using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Blocks.Messaging.MassTransit;

namespace ArticleHub.Application;

public static class DependencyInjection
{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
		{
				services
						.AddMemoryCache()
						.AddMassTransit(configuration, Assembly.GetExecutingAssembly());
				
				return services;
		}
}
