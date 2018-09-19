using System;
using System.Collections.Generic;
using Polly.Caching;

namespace SchoolExperienceEvents.Implementation
{
    /// <summary>
    /// Implements an <see cref="ISyncCacheProvider{T}"/> as a simple dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the cached data.</typeparam>
    /// <seealso cref="Polly.Caching.ISyncCacheProvider{T}" />
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

        /// <summary>
        /// Gets a value from cache.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// The value from cache; or null, if none was found.
        /// </returns>
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

        /// <summary>
        /// Puts the specified value in the cache.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to put into the cache.</param>
        /// <param name="ttl">The time-to-live for the cache entry.</param>
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
