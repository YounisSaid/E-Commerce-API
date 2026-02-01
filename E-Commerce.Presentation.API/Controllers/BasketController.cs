using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    public class BasketController(IServiceManager service) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById([FromQuery] string id)
        {
            var result = await service.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var result = await service.BasketService.CreateBasketAsync(basket,TimeSpan.FromDays(1));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketByid(string id)
        {
            var result = await service.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
