using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auth.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Data.Test;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AuthDBContext>();
				using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				context.SeedTestData(userManager);
    }

    public static void SeedTestData(this AuthDBContext context, UserManager<User> userManager)
    {
        const string DefaultPassword = "Pass.123!";
        using var transaction = context.Database.BeginTransaction();

				var users = context.LoadFromJson<User>();
        foreach (var user in users) 
        {
            user.UserName = user.Email;
						var result = userManager.CreateAsync(user, DefaultPassword).GetAwaiter().GetResult();
				}

        transaction.Commit();
    }
}
