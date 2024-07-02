using Microsoft.Extensions.Caching.Memory;

namespace Articles.System;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private MemoryCacheEntryOptions _cacheOptions;
    private static readonly AsyncLock _mutex = new AsyncLock();

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));

    }
    public bool TryGet<T>(string cacheKey, out T value)
    {
        using (_mutex.Lock())
        {
            _memoryCache.TryGetValue(cacheKey, out value);
            return value != null;
        }
    }
    public T GetOrCreate<T>(string cacheKey, T value)
    {
        return _memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.SetOptions(_cacheOptions);
            return value;
        });
    }
    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<ICacheEntry, Task<T>> factory)
    {
        return await _memoryCache.GetOrCreateAsync(cacheKey, factory);
    }

    public T GetOrCreate<T>(string cacheKey, Func<ICacheEntry, T> factory)
    {
        return _memoryCache.GetOrCreate(cacheKey, factory);
    }

    public T Set<T>(string cacheKey, T value)
    {
        return _memoryCache.Set(cacheKey, value, _cacheOptions);
    }
    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}
