using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Service.Abstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync (string Id);
        Task<CustomerBasketDto> CreateBasketAsync (CustomerBasketDto basket, TimeSpan duration);
        Task<bool> DeleteBasketAsync (string Id);
    }
}
