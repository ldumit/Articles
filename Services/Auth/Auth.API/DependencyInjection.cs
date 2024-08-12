using Auth.Domain.Models;
using Auth.Persistence;
using Microsoft.AspNetCore.Identity;


namespace Auth.Application;

public static class DependenciesConfiguration
{
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
}
