using E_commerce.Domain.Entites.Orders;

namespace E_Commerce.Service.Specifications.Orders
{
    public class OrderSpecification : BaseSpecificiation<Order>
    {
        public OrderSpecification(Guid id,string email) : base(o=>o.Id==id && o.Email==email)
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.DeliveryMethod);
        }

        public OrderSpecification(string email) : base (o=> o.Email == email)
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.DeliveryMethod);

            AddOrderByDescending(o => o.OrderDate);
        }
    }
}
