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

				services.AddMassTransit(configurator =>
				{
						if (assembly != null)
								configurator.AddConsumers(assembly);

						configurator.UsingRabbitMq((context, cfg) =>
						{
								// Configure RabbitMQ connection
								cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
								{
										h.Username(rabbitMqOptions.UserName);
										h.Password(rabbitMqOptions.Password);
								});

								cfg.ConfigureEndpoints(context);
						});
				});


				return services;
		}
}
