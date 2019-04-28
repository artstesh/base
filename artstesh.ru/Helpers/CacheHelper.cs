using System;
using System.Threading.Tasks;
using C2c.Config;
using C2c.Services.Converters;
using Microsoft.Extensions.Caching.Distributed;

namespace C2c.Helper
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IDistributedCache _cache;
        private readonly IConfigSettings _settings;

        public CacheHelper(IDistributedCache cache, IConfigSettings settings)
        {
            _cache = cache;
            _settings = settings;
        }

        public async Task<byte[]> Get(string key)
        {
            return await _cache.GetAsync(key);
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task Set(string key, object obj, int cacheLifeTime =-1)
        {
            if (cacheLifeTime == -1) cacheLifeTime = _settings.ApplicationKeys.CacheLifeTime;
            await _cache.SetAsync(key, ObjectByteConverter.ObjectToByteArray(obj),
                new DistributedCacheEntryOptions()
                    {AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheLifeTime)});
        }
    }
}