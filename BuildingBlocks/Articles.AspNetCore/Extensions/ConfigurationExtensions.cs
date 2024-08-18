using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.AspNetCore;

public static class ConfigurationExtensions
{
		public static IServiceCollection ConfigureOptions<TOptions>(this IServiceCollection services, ConfigurationManager configuration)
				where TOptions : class
				=> services.Configure<TOptions>(configuration.GetSection(typeof(TOptions).Name));
}
