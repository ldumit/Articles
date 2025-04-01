using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Blocks.Core;

public static class ConfigurationExtensions
{
		public static IServiceCollection ConfigureOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
				where TOptions : class
		{
				var section = configuration.GetSection(typeof(TOptions).Name);
				//todo - guard against missing section
				if (!section.Exists())
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
