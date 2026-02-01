using E_Commerce.Serviece.Abstraction;

namespace E_Commerce.Service.Abstraction
{
    public interface IServiceManager    
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public ICacheService CacheService { get; }
        public IAuthService AuthService { get; }
        public IOrderService OrderService { get; }
        public IPaymentService PaymentService { get; }
    }
}
