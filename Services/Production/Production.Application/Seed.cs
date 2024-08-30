using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Production.Domain.Entities;
using Articles.EntityFrameworkCore;

namespace Production.Application;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<Persistence.ProductionDbContext>();
        context.SeedTestData();
    }

    public static void SeedTestData(this Persistence.ProductionDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

				context.Seed<User>();
				context.Seed<Typesetter>();
				context.Seed<Journal>();

        transaction.Commit();

				context.Seed<Article>();
    }
}
