using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Services.Auth;
using E_Commerce.Service.Services.Baskets;
using E_Commerce.Service.Services.Cache;
using E_Commerce.Service.Services.Orders;
using E_Commerce.Service.Services.Payment;
using E_Commerce.Service.Services.Products;
using E_Commerce.Serviece.Abstraction;
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
                               IOptions<JwtOptions> options,
                               IConfiguration configuration) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManager, options, mapper, unitOfWork);

        public IOrderService OrderService { get; } = new OrderService(unitOfWork, mapper, basketRepository);

        public IPaymentService PaymentService { get; } = new PaymentService(unitOfWork, basketRepository, configuration, mapper);
    }
}