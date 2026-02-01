namespace E_Commerce.Shared.Dtos.Orders
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
      
        public OrderAddressDto OrderAddressDto { get; set; } = null!;
        public string DeliveryMethod { get; set; }
       
        public ICollection<OrderItemDto> OrderItems { get; set; }

        public double SubTotal { get; set; }

        public double Total { get; set; }

    }
}
