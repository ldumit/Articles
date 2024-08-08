using Auth.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Articles.AspNetCore.Dependencies;

public static class ConfigureAuthentication
{

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
				//todo create an extension method which will use nameof ans substract "Options" word
				var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();

				services
						.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
						.AddJwtBearer(JwtOptions =>
						{
								JwtOptions.RequireHttpsMetadata = false;
								JwtOptions.SaveToken = true;
								JwtOptions.TokenValidationParameters = new TokenValidationParameters
								{
										ValidateIssuer =  true,
										ValidIssuer = jwtOptions.Issuer,
										ValidateIssuerSigningKey = true,
										IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
										ValidateAudience = false,
										RequireExpirationTime = true,
								};
						});

				return services;
		}
}
