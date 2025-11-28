using AutoMapper;
using E_commerce.Domain.Contracts;
using E_Commerce.Service.Abstraction;

//using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Baskets;
using E_Commerce.Service.Services;

namespace E_Commerce.Service
{
    public class ServiceManger(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IServiceManager
    {
        public IProductService ProductServices { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketServices { get; } = new BasketService(basketRepository, mapper);
    }
}
