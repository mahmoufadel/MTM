﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Cache.Redis
{
    public class MemoryCacheManager : ICacheManager
    {
        public T Get<T>(string cacheKey) where T : class
        {
            if (MemoryCache.Default[cacheKey] != null)
            {
                return (T)MemoryCache.Default[cacheKey];
            }
            return null;
        }

        public async Task<T> GetAsync<T>(string cacheKey) where T : class
        {
            if (MemoryCache.Default[cacheKey] != null)
            {
                return (T)MemoryCache.Default[cacheKey];
            }

            return null;
        }

        public bool Add<T>(string cacheKey, T data) where T : class
        {
            return AddToCache(cacheKey, data);
        }

        public async Task<bool> AddAsync<T>(string cacheKey, T data) where T : class
        {
            var result = await Task.Run(() => AddToCache(cacheKey, data));
            return result;
        }
        private bool AddToCache(string cacheKey, object value, DateTime? absoluteExpiration = null)
        {
            try
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration =
                    DateTimeOffset.Now.AddSeconds(60000000.0);

                MemoryCache.Default.Add(cacheKey, value, policy);

                return true;
            }
            catch
            {
                return false;
            }

        }

       

        public async Task<bool> Clear()
        {
            return false;
        }

       

		public async Task<bool> Remove(string cacheKey)
		{
            MemoryCache.Default.Remove(cacheKey);
            return true;
        }
	}
}
