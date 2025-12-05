namespace E_Commerce.Shared.Dtos.Orders
{
    public class DeliveryMethodResponseDto
    {
        public string ShortName { get; set; }
        public string Description { get; set; }

        public string DeliveryTime { get; set; }
        public double Price { get; set; }
    }
}
