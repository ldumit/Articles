using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Articles.System;

public class ConfigureOptions<TOptions>(IConfiguration _configuration) 
		: IConfigureOptions<TOptions> where TOptions : class
{
		public void Configure(TOptions options)
		{
				_configuration.GetSection(nameof(TOptions)).Bind(options);
		}
}
