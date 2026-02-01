using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Serviece.Abstraction
{
    public interface IPaymentService
    {
        public Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
        public Task UpdateOrderPaymentStatusAsync(string json, string header);
    }
}
