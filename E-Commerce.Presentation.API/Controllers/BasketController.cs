using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    public class BasketController(IServiceManager service) : APIBaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBasketAsync(string Id)
        {
            var result = await service.BasketServices.GetBasketAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasketAsync(CustomerBasketDto basket)
        {
            var result = await service.BasketServices.CreateBasketAsync(basket,TimeSpan.FromDays(1));
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBasketAsync(string Id)
        {
            var result = await service.BasketServices.DeleteBasketAsync(Id);
            return NoContent();
        }
    }
}
