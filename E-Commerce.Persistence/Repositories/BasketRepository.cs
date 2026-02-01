using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Baskets;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty
                ? null
                : JsonSerializer.Deserialize<CustomerBasket>(data!);
        }

        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            EnsureBasketId(basket);

            var serializedBasket = JsonSerializer.Serialize(basket);
            var isCreated = await _database.StringSetAsync(basket.Id, serializedBasket, duration);

            return isCreated ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        private static void EnsureBasketId(CustomerBasket basket)
        {
            if (string.IsNullOrEmpty(basket.Id))
            {
                basket.Id = Guid.NewGuid().ToString();
            }
        }
    }
}