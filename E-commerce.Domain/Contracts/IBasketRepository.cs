using E_commerce.Domain.Entites.Baskets;

namespace E_commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket,TimeSpan duration);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
