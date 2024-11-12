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

				context.Seed<Person>();

				context.Seed<Journal>();

				context.Seed<Article>();
				
        context.Seed<AssetCurrentFileLink>(); // this is a link between an asset and a file, which couldn't be included in Article seeding

				transaction.Commit();
		}
}
