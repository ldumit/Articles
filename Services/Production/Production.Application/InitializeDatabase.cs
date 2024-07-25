using Articles.Entitities;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Production.Domain.Entities;

namespace Production.Application;

public static class InitializeDatabase
{
    public static void Migrate(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<Database.DbContext>();
        context.Database.Migrate();
    }

    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<Database.DbContext>();
        context.SeedTestData();
    }

    public static void SeedTestData(this Database.DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

        Seed<User>(context);
        Seed<Typesetter>(context);
        Seed<Journal>(context);

        transaction.Commit();

        Seed<Article>(context);
    }

    private static void Seed<TEntity>(Database.DbContext context)
        where TEntity : Entity<int>
    {

        if (context.Set<TEntity>().Any())
            return;

        var filePath = $"{AppContext.BaseDirectory}TestData/{typeof(TEntity).Name}.json";
        if (System.IO.File.Exists(filePath))
        {
            var collection = JsonConvert.DeserializeObject<TEntity[]>(System.IO.File.ReadAllText(filePath));
            if (collection != null)
                context.Set<TEntity>().AddRange(collection);
        }
        context.SaveChanges();
    }
}
