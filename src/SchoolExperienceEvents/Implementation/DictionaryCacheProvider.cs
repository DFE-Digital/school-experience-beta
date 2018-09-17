using System;
using System.Collections.Generic;
using Polly.Caching;

namespace SchoolExperienceEvents.Implementation
{
    public class DictionaryCacheProvider<T> : ISyncCacheProvider<T>
        where T : class
    {
        private class CacheItem
        {
            public T Value { get; set; }
            public DateTime ExpiresAt { get; set; }
            public Ttl Ttl { get; set; }
        }

        private readonly IDictionary<string, CacheItem> _cacheEntries = new Dictionary<string, CacheItem>();

        public T Get(string key)
        {
            if (!_cacheEntries.TryGetValue(key, out var item))
            {
                return null;
            }

            var now = DateTime.UtcNow;

            if (now > item.ExpiresAt)
            {
                return null;
            }

            if (item.Ttl.SlidingExpiration)
            {
                item.ExpiresAt = now + item.Ttl.Timespan;
            }

            return item.Value;
        }

        public void Put(string key, T value, Ttl ttl)
        {
            var now = DateTime.UtcNow;

            _cacheEntries[key] = new CacheItem
            {
                Value = value,
                ExpiresAt = now + ttl.Timespan,
                Ttl = ttl,
            };
        }
    }
}
