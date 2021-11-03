using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading.Tasks;

namespace MTM.Cache.Redis
{
	public class RedisCacheManager : ICacheManager
	{
		private readonly IRedisCacheClient _redisCacheClient;
		public RedisCacheManager(IRedisCacheClient redisCacheClient)
		{
			_redisCacheClient = redisCacheClient;
		}
		/*public T Get<T>(string cacheKey) where T : class
        {
            return _redisCacheClient.Db0.Get<T>(cacheKey);           
        }*/

		public async Task<T> GetAsync<T>(string cacheKey) where T : class
		{
			return await _redisCacheClient.Db0.GetAsync<T>(cacheKey);
		}

		/*public bool Add<T>(string cacheKey, T data) where T : class
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                RedisCacheDatabaseMaster.StringSet(cacheKey, JsonConvert.SerializeObject(data, Formatting.None, settings));
                return true;
            }
            catch
            {
                return false;
            }
        }*/

		public async Task<bool> AddAsync<T>(string cacheKey, T data) where T : class
		{
			return await _redisCacheClient.Db0.AddAsync<T>(cacheKey, data);
		}

		public async Task<bool> Remove(string cacheKey)
		{
			return await _redisCacheClient.Db0.RemoveAsync(cacheKey);
		}

		public async Task<bool> Clear()
		{
			try
			{
				await _redisCacheClient.Db0.FlushDbAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
