using System.Net.Mail;

namespace E_commerce.Domain.Entites.Orders
{
    public class Order : Entity<Guid>
    {
        public Order()
        { }
        public Order(string email, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, double subTotal, string? paymentIntentId)
        {
            Email = email;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string Email { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public double SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }

        public double GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
