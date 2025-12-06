namespace E_Commerce.Shared.Dtos.Orders
{
    public class OrderRequestDto
    {
        public string BasketId { get; set; } = null!;
        public int DeliveryMethodId { get; set; }

        public OrderAddressDto OrderAddressDto { get; set; } = null!;
    }
}
