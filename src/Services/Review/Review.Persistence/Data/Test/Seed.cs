using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Review.Persistence.Data.Test;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        //todo - make this generic using DbContext as a constraint
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ReviewDbContext>();
        context.SeedTestData();
    }

    public static void SeedTestData(this ReviewDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

        context.SeedFromJson<Person>();
        context.SeedFromJson<Journal>();

        transaction.Commit();
    }
}
