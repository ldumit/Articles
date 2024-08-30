using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Articles.System;

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
								return JsonConvert.DeserializeObject<T>(cachedData);

						var item = await createItem();
						var serializedData = JsonConvert.SerializeObject(item);

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
								return JsonConvert.DeserializeObject<T>(cachedData);

						var item = createItem();
						var serializedData = JsonConvert.SerializeObject(item);

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
								return JsonConvert.DeserializeObject<T>(cachedData);

						var serializedData = JsonConvert.SerializeObject(item);

						if (options == null)
								options = new DistributedCacheEntryOptions();

						cache.SetString(key, serializedData, options);

						return item;
				}

		}
}