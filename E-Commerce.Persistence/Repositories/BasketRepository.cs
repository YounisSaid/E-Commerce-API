using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Baskets;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var basketRedis = await _database.StringGetAsync(Id);
            if(basketRedis.IsNullOrEmpty) return null;

            var CustomerBasket = JsonSerializer.Deserialize<CustomerBasket>(basketRedis);
            if (CustomerBasket is null) return null;

            return  CustomerBasket;
        }
        public async Task<CustomerBasket> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);  
            if(serializedBasket is null) return null;
            var flag = await _database.StringSetAsync(basket.Id, serializedBasket, duration);
            if(!flag) return null;
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return  await _database.KeyDeleteAsync(Id);
        }

    }
}
