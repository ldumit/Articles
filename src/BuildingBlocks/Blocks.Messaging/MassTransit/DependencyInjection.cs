using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Blocks.Core;

namespace Blocks.Messaging.MassTransit;

public static class DependencyInjection
{
		public static IServiceCollection AddMassTransit
				(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
		{
				var rabbitMqOptions = configuration.GetByTypeName<RabbitMqOptions>();

				services.AddMassTransit(config =>
				{
						if (assembly != null)
								config.AddConsumers(assembly);

						config.UsingRabbitMq((context, rabbitConfig) =>
						{
								// Configure RabbitMQ connection
								rabbitConfig.Host(new Uri(rabbitMqOptions.Host), rabbitMqOptions.VirtualHost, h =>
								{
										h.Username(rabbitMqOptions.UserName);
										h.Password(rabbitMqOptions.Password);
								});

								rabbitConfig.ConfigureEndpoints(context);
						});
				});


				return services;
		}
}
