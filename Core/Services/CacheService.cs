using Microsoft.Extensions.Caching.Memory;

namespace test_webapi.Core.Services
{
    public interface ICacheService
    {
        T? GetOrCreate<T>(string key, Func<T> factory, TimeSpan? expiration = null);
        void Remove(string key);
    }

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(10);

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? GetOrCreate<T>(string key, Func<T> factory, TimeSpan? expiration = null)
        {
            if (_cache.TryGetValue(key, out T? value))
                return value;

            value = factory();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(expiration ?? _defaultExpiration);

            _cache.Set(key, value, cacheEntryOptions);

            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
