using System.Threading.Tasks;

namespace MTM.Cache.Redis
{
	public interface ICacheManager
	{
		Task<bool> AddAsync<T>(string cacheKey, T data) where T : class;
		Task<bool> Clear();
		Task<T> GetAsync<T>(string cacheKey) where T : class;
		Task<bool> Remove(string cacheKey);
	}
}