using Articles.Entitities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auth.Domain.Models;
using Auth.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Articles.System;

namespace Auth.Application;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				context.SeedTestData(userManager);
    }

    public static void SeedTestData(this ApplicationDbContext context, UserManager<User> userManager)
    {
        const string DefaultPassword = "Pass.123!";
        using var transaction = context.Database.BeginTransaction();

				var users = LoadFromJson<User>(context);
        foreach (var user in users) 
        {
            user.UserName = user.Email;
						var result = userManager.CreateAsync(user, DefaultPassword).GetAwaiter().GetResult();
				}

        transaction.Commit();
    }

    private static TEntity[] LoadFromJson<TEntity>(ApplicationDbContext context)
        where TEntity : class, IEntity<int>
    {
        if (context.Set<TEntity>().Any())
            return Array.Empty<TEntity>();

        var filePath = $"{AppContext.BaseDirectory}TestData/{typeof(TEntity).Name}.json";
        if (File.Exists(filePath))
        {
            var collection = JsonExtensions.DeserializeCaseInsensitive<TEntity[]>(System.IO.File.ReadAllText(filePath));
            if (collection != null)
                return collection;

				}
        return Array.Empty<TEntity>();
    }
}
