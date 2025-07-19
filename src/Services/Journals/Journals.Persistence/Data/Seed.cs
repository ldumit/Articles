using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Redis.OM;
using Blocks.Redis;
using Journals.Domain.Journals;

namespace Journals.Persistence.Data;

public static class Seed
{
    public static async Task SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
				var provider = scope.ServiceProvider.GetRequiredService<RedisConnectionProvider>();
				var redis = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer> ();
				await provider.SeedTestData(redis.GetDatabase());
    }

    public static async Task SeedTestData(this RedisConnectionProvider provider, IDatabase redisDb)
    {
				await provider.SeedFromJson<Editor>(redisDb);
				await provider.SeedFromJson<Journal>(redisDb);
		}
}
