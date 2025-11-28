using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Baskets;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;

namespace E_Commerce.Service.Baskets
{
    public class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
    {
        public async Task<CustomerBasketDto> GetBasketAsync(string Id)
        {
            var basket = await basketRepository.GetBasketAsync(Id);
            if (basket is null) throw new BasketNotFoundException();
            return mapper.Map<CustomerBasketDto>(basket);
        }
        public async Task<CustomerBasketDto> CreateBasketAsync(CustomerBasketDto basket, TimeSpan duration)
        {
            var basketEntity = mapper.Map<CustomerBasket>(basket);

            var newBasket = await basketRepository.CreateBasketAsync(basketEntity, duration);
            if(newBasket is null) throw new CreateOrUpdateBadRequestException();

            return mapper.Map<CustomerBasketDto>(newBasket); ;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            var flag = await basketRepository.DeleteBasketAsync(Id);
            if(!flag) throw new DeleteBadRequestException();
            return flag;
        }

    }
}
