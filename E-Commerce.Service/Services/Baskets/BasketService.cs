using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Baskets;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Service.Services.Baskets
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);

            if (basket is null)
                throw new BasketNotFoundException(basketId);

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> CreateBasketAsync(CustomerBasketDto basketDto, TimeSpan expiration)
        {
            var basketEntity = mapper.Map<CustomerBasket>(basketDto);
            var persistedBasket = await basketRepository.CreateBasketAsync(basketEntity, expiration);

            if (persistedBasket is null)
                throw new CreateOrUpdateBadRequestException();

            return mapper.Map<CustomerBasketDto>(persistedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var isDeleted = await basketRepository.DeleteBasketAsync(basketId);

            if (!isDeleted)
                throw new DeleteBadRequestException();

            return isDeleted;
        }
    }
}