using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Articles.System;

public class ConfigureOptions<TOptions>(IConfiguration _configuration) 
		: IConfigureOptions<TOptions> where TOptions : class
{
		//private const string SectionName = "Jwt";
		//private readonly IConfiguration _configuration;

		//public ConfigureOptions(IConfiguration configuration)
		//{
		//		_configuration = configuration;
		//}

		public void Configure(TOptions options)
		{
				_configuration.GetSection(nameof(TOptions)).Bind(options);
		}
}
