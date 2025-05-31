using Articles.Security;
using Auth.API;
using Auth.API.Mappings;
using Auth.Domain.Models;
using Auth.Persistence;
using Blocks.Security;
using EmailService.Contracts;
using EmailService.Smtp;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Auth.Application;

//todo move it to Application project
public static class DependenciesConfiguration
{
		public static void ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddAndValidateOptions<EmailOptions>(configuration)
						.AddAndValidateOptions<JwtOptions>(configuration)
						.ConfigureOptions<PostConfigureJwtBearerOptions>()
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddFastEndpoints()
						.AddEndpointsApiExplorer()                  // Minimal API docs (Swagger)
						.AddAutoMapper(new Assembly[] { typeof(Auth.API.Features.CreateUserCommandMapping).Assembly })
						.AddSwaggerGen()                            // Swagger setup
						.AddJwtAuthentication(configuration)        // JWT Authentication
						.AddAuthorization()                         // Authorization configuration
						.AddJwtIdentity(configuration);

				services.AddGrpc();

				services.AddScoped<IEmailService, SmtpEmailService>();
				services.AddScoped<TokenFactory>();

				return services;
		}


		public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
		{
				services.AddIdentity<User, Domain.Models.Role>(options =>
				{
						// Lockout settings
						options.Lockout.AllowedForNewUsers = true;
						options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
						options.Lockout.MaxFailedAccessAttempts = 5;

						options.User.RequireUniqueEmail = false; //to-do - change back to true after test training users not needed anymore
																										 //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
				})
				.AddEntityFrameworkStores<AuthDBContext>()
				.AddSignInManager<SignInManager<User>>()
				.AddDefaultTokenProviders();

				services.AddSingleton<GrpcTypeAdapterConfig>();

				return services;
		}
}
