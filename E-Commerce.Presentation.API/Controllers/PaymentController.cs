using E_Commerce.Service;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.Presentation.API.Controllers
{
    public class PaymentController(IServiceManager serviceManger) : APIBaseController
    {
        [HttpPost("{basketId}")]
        public async Task<CustomerBasketDto> CreatePayment(string basketId)
        {
            var result = await serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return result;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var signatureHeader = Request.Headers["Stripe-Signature"];

            await serviceManger.PaymentService.UpdateOrderPaymentStatusAsync(json, signatureHeader);
           
            return new EmptyResult();
        }
    }
}