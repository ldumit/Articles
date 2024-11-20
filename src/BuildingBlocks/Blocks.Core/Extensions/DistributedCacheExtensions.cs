using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Blocks.Core;

public static class DistributedCacheExtensions
{
		private static readonly AsyncLock _mutex = new AsyncLock();

		public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> createItem, DistributedCacheEntryOptions options = null)
		{
				//todo - optimize it using double check lock
				using (_mutex.Lock())
				{
						var cachedData = await cache.GetStringAsync(key);
						if (cachedData != null)
								return JsonSerializer.Deserialize<T>(cachedData);

						var item = await createItem();
						var serializedData = JsonSerializer.Serialize(item);

						if (options == null)
								options = new DistributedCacheEntryOptions();

						await cache.SetStringAsync(key, serializedData, options);

						return item;
				}
		}

		public static T GetOrCreate<T>(this IDistributedCache cache, string key, Func<T> createItem, DistributedCacheEntryOptions options = null)
		{
				//todo - optimize it using double check lock
				using (_mutex.Lock())
				{
						var cachedData = cache.GetString(key);
						if (cachedData != null)
								return JsonSerializer.Deserialize<T>(cachedData);

						var item = createItem();
						var serializedData = JsonSerializer.Serialize(item);

						if (options == null)
								options = new DistributedCacheEntryOptions();

						cache.SetString(key, serializedData, options);

						return item;
				}
		}

		public static T GetOrCreate<T>(this IDistributedCache cache, string key, T item, DistributedCacheEntryOptions? options = null)
		{
				//todo - optimize it using double check lock
				using (_mutex.Lock())
				{
						var cachedData = cache.GetString(key);
						if (cachedData != null)
								return JsonSerializer.Deserialize<T>(cachedData);

						var serializedData = JsonSerializer.Serialize(item);

						if (options == null)
								options = new DistributedCacheEntryOptions();

						cache.SetString(key, serializedData, options);

						return item;
				}

		}
}