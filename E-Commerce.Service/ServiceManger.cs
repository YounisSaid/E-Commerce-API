using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Auth;


//using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.Cache;
using E_Commerce.Service.Services;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Serviece.Abstraction.Auth;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Service
{
    public class ServiceManger(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               IBasketRepository basketRepository,
                               ICacheRepository cacheRepository,
                               UserManager<AppUser> userManager) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}
