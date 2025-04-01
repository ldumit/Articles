using Journals.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redis.OM;
using Blocks.Redis;
using StackExchange.Redis;

namespace Journals.Persistence.Data.Test;

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
				await provider.Seed<Editor>(redisDb);
				await provider.Seed<Journal>(redisDb);
		}
}
