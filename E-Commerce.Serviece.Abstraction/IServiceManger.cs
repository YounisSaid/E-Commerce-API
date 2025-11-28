using E_Commerce.Serviece.Abstraction;
using E_Commerce.Serviece.Abstraction.Auth;

namespace E_Commerce.Service.Abstraction
{
    public interface IServiceManager    
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public ICacheService CacheService { get; }
        public IAuthService AuthService { get; }
    }
}
