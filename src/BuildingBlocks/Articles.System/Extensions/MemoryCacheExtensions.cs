using Microsoft.Extensions.Caching.Memory;

namespace Blocks.Core;

public static class MemoryCacheExtensions
{
		public static T GetOrCreate<T>(this IMemoryCache memoryCache, Func<ICacheEntry, T> factory)
				=> memoryCache.GetOrCreate(typeof(T).FullName, factory);
}
