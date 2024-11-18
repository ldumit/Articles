using Blocks.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.AspNetCore;

public static class ConfigurationExtensions
{
		public static IServiceCollection ConfigureOptions<TOptions>(this IServiceCollection services, ConfigurationManager configuration)
				where TOptions : class
		{
				var section = configuration.GetSection(typeof(TOptions).Name);
				if(!section.Exists())
						throw new InvalidOperationException($"Configuration section '{section.Key}' is missing.");
				return services.Configure<TOptions>(section);
		}

		public static T GetByTypeName<T>(this IConfiguration configuration)
		{
				var sectionName = typeof(T).Name;
				var section = configuration.GetSection(sectionName).Get<T>()!;

				return Guard.AgainstNull(section, sectionName);
		}
}
