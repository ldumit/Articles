using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Blocks.Messaging.MassTransit;
using Blocks.Mapster;

namespace ArticleHub.Application;

public static class DependencyInjection
{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
		{
				services
						.AddMemoryCache()
						.AddMapsterConfigsFromCurrentAssembly()
						.AddMassTransitWithRabbitMQ(configuration, assembly ?? Assembly.GetExecutingAssembly());
				
				return services;
		}
}
