using Journals.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redis.OM;
using Articles.Redis;

namespace Journals.Persistence.TestData;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
				var provider = scope.ServiceProvider.GetRequiredService<RedisConnectionProvider>();
				provider.SeedTestData();
    }

    public static void SeedTestData(this RedisConnectionProvider provider)
    {
				provider.Seed<Editor>();
				provider.Seed<Journal>();
		}
}
