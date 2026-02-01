using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Serviece.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto request, string email);
        Task<IEnumerable<DeliveryMethodResponseDto>> GetAllDeliveryMethodsAsync();
        Task<OrderResponseDto> GetOrderForSpecificUserByIdAsync(Guid Id, string email);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersForSpecificUserAsync(string email);

    }
}
