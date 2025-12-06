using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Auth;


//using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.Cache;
using E_Commerce.Service.Orders;
using E_Commerce.Service.Services;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Serviece.Abstraction.Auth;
using E_Commerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace E_Commerce.Service
{
    public class ServiceManger(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               IBasketRepository basketRepository,
                               ICacheRepository cacheRepository,
                               UserManager<AppUser> userManager,
                               IOptions<JwtOptions> options) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManager, options);

        public IOrderService OrderService { get; } = new OrderService(unitOfWork,mapper, basketRepository);
    }
}
