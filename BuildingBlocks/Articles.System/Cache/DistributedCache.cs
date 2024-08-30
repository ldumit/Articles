using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Articles.System;
using Newtonsoft.Json.Linq;

namespace Articles.System.Cache
{
		//public class DistributedCache(IDistributedCache _cache) : ICache
		//{
		//		MemoryDistributedCache
		//		private static readonly AsyncLock _mutex = new AsyncLock();
		//		private DistributedCacheEntryOptions _cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));

		//		public T GetOrCreate<T>(string cacheKey, T value)
		//		{
		//				return _cache.GetOrCreate(cacheKey, value, _cacheOptions);
		//		}

		//		public T GetOrCreate<T>(string cacheKey, Func<T> factory)
		//		{
		//				return _cache.GetOrCreate(cacheKey, factory, _cacheOptions);
		//		}

		//		public Task<T> GetOrCreateAsync<T>(string cacheKey, Func<ICacheEntry, Task<T>> factory)
		//		{
		//				throw new NotImplementedException();
		//		}
		//		public async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> createItem, DistributedCacheEntryOptions options = null)
		//		{
		//				//todo - optimize it using double check lock
		//				using (_mutex.Lock())
		//				{
		//						var cachedData = await cache.GetStringAsync(key);
		//						if (cachedData != null)
		//								return JsonConvert.DeserializeObject<T>(cachedData);

		//						var item = await createItem();
		//						var serializedData = JsonConvert.SerializeObject(item);

		//						if (options == null)
		//								options = new DistributedCacheEntryOptions();

		//						await cache.SetStringAsync(key, serializedData, options);

		//						return item;
		//				}
		//		}


		

		//		public void Remove(string cacheKey)
		//		{
		//				throw new NotImplementedException();
		//		}

		//		public T Set<T>(string cacheKey, T value)
		//		{
		//				throw new NotImplementedException();
		//		}

		//		public bool TryGet<T>(string cacheKey, out T value)
		//		{
		//				throw new NotImplementedException();
		//		}
		//}
}
