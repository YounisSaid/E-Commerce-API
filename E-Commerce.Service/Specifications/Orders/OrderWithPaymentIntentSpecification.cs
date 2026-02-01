using E_commerce.Domain.Entites.Orders;

namespace E_Commerce.Service.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecification : BaseSpecificiation<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentID) : base(o=>o.PaymentIntentId==paymentIntentID)
        {
        }
    }
}
