﻿using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using FastEndpoints.Swagger;
using Articles.Security;
using Blocks.Security;
using EmailService.Contracts;
using EmailService.Smtp;
using Auth.API;
using Auth.API.Mappings;
using Auth.Persistence;
using Auth.API.Features;

namespace Auth.Application;

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
				services.AddControllers();

				services
						.AddFastEndpoints()
						.SwaggerDocument()
						.AddEndpointsApiExplorer()                  // Minimal API docs (Swagger)
						.AddAutoMapper(new Assembly[] { typeof(CreateUserCommandMapping).Assembly })
						.AddSwaggerGen()                            // Swagger setup
						.AddJwtIdentity(configuration)
						.AddJwtAuthentication(configuration)        // JWT Authentication
						.AddAuthorization();                         // Authorization configuration

				services.AddGrpc();

				services.AddSingleton<IEmailService, SmtpEmailService>();
				services.AddScoped<TokenFactory>();

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
