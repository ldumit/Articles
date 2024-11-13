using Blocks.Redis;
using Journals.Persistence;
using Redis.OM;
using StackExchange.Redis;

namespace Journals.API;

public static class DependencyInjection
{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
				var connectionString = configuration.GetConnectionString("Database");

				services.AddSingleton(new RedisConnectionProvider(connectionString!));
				var redis = ConnectionMultiplexer.Connect(connectionString!.Replace("redis://", ""));
				services.AddSingleton<IConnectionMultiplexer>(redis);

				services.AddSingleton<JournalDbContext>();
				services.AddScoped(typeof(Repository<>));

				return services;
		}
}
