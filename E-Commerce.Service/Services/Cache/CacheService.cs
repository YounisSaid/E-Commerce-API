using E_commerce.Domain.Contracts;
using E_Commerce.Serviece.Abstraction;

namespace E_Commerce.Service.Services.Cache
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsync(key, value, duration);
        }
    }
}
