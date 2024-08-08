using Auth.Application;
using Microsoft.Extensions.Options;

namespace Auth.API;

public class ConfigureJwtOptions : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public ConfigureJwtOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
