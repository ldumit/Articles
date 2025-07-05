using Articles.Security;
using Auth.API;
using Auth.API.Features.Persons;
using Auth.API.Features.Users.CreateAccount;
using Auth.API.Mappings;
using Auth.Grpc;
using Auth.Persistence;
using EmailService.Smtp;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Auth.Api;

public static class DependenciesConfiguration
{
		public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
		{
				services
						.AddAndValidateOptions<JwtOptions>(configuration)
						.ConfigureOptions<PostConfigureJwtBearerOptions>()
						.Configure<JsonOptions>(opt =>
						{
								opt.SerializerOptions.PropertyNameCaseInsensitive = true;
								opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
						});

				return services;
		}

		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
		{
				services.AddControllers();

				services
						.AddFastEndpoints()
						.SwaggerDocument()
						.AddEndpointsApiExplorer()											// Minimal API docs (Swagger)
						.AddAutoMapper([typeof(CreateUserCommandMapping).Assembly])
						.AddSwaggerGen()																// Swagger setup
						.AddJwtIdentity(config)
						.AddJwtAuthentication(config)										// JWT Authentication
						.AddAuthorization();														// Authorization configuration

				services.AddCodeFirstGrpc(options =>
				{
						options.ResponseCompressionLevel = CompressionLevel.Fastest;
						options.EnableDetailedErrors = true;
				});

				services.AddSmtpEmailService(config);

				services.AddScoped<IPersonService, PersonGrpcService>();

				return services;
		}


		public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
		{
				services.AddIdentityCore<User>(options =>
				{
						// Lockout settings
						options.Lockout.AllowedForNewUsers = true;
						options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
						options.Lockout.MaxFailedAccessAttempts = 5;

						options.User.RequireUniqueEmail = false; //to-do - change back to true after test training users not needed anymore
						//options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
				})
			  .AddRoles<Auth.Domain.Roles.Role>()
				.AddEntityFrameworkStores<AuthDBContext>()
				.AddSignInManager<SignInManager<User>>()
				.AddDefaultTokenProviders();

				services.Configure<IdentityOptions>(options =>
				{
						options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
				});

				services.AddSingleton<GrpcTypeAdapterConfig>();

				return services;
		}
}
