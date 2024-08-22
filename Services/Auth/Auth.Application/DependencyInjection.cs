using Auth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Models;
using EmailService.Contracts;
using EmailService.Smtp;
using Articles.Security;
using GraphQL.Types;
using Auth.Application.GraphQLSchemas;

namespace Auth.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
				services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));


				///services.AddScoped<UserManager<User>>();
				//services.AddScoped<SignInManager<User>>();
				services.AddScoped<IEmailService, SmtpEmailService>();
				services.AddScoped<TokenFactory>();


				services.AddScoped<ISchema, UserSchema>();

				return services;
    }

		public static void AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
		{

				services.AddIdentity<User, Role>(options =>
				{
						// Lockout settings
						options.Lockout.AllowedForNewUsers = true;
						options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
						options.Lockout.MaxFailedAccessAttempts = 5;

						options.User.RequireUniqueEmail = false; //to-do - change back to true after test training users not needed anymore
																										 //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
				})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddSignInManager<SignInManager<User>>()
				.AddDefaultTokenProviders();
		}

		//public static void AddSqlServerDbContext<TDbContext>(this IServiceCollection services, string connectionString)
		//		where TDbContext : DbContext
		//{
		//		if (string.IsNullOrEmpty(connectionString))
		//		{
		//				throw new ArgumentException(nameof(connectionString));
		//		}
		//		services.AddDbContext<TDbContext>(opts => opts.UseSqlServer(connectionString));
		//}
}
