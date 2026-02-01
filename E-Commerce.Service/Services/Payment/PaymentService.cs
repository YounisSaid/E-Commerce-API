using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Orders;
using E_commerce.Domain.Entites.Products;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Specifications.Orders;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace E_Commerce.Service.Services.Payment
{
    public class PaymentService(
        IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        IConfiguration configuration,
        IMapper mapper) : IPaymentService
    {
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var basket = await GetBasketOrThrowAsync(basketId);

            await SynchronizeBasketPricesAsync(basket);

            decimal shippingPrice = await CalculateShippingPriceAsync(basket);

            long totalAmountInCents = CalculateTotalInCents(basket, shippingPrice);

            await ApplyStripePaymentIntentAsync(basket, totalAmountInCents);

            // Update Redis/DB with the intent IDs and correct prices
            await basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatusAsync(string json, string header)
        {
            var stripeEvent = ConstructStripeEvent(json, header);
            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            var task = stripeEvent.Type switch
            {
                EventTypes.PaymentIntentSucceeded => UpdateOrderStatusAsync(paymentIntent.Id, OrderStatus.Delivered),
                EventTypes.PaymentIntentPaymentFailed => UpdateOrderStatusAsync(paymentIntent.Id, OrderStatus.Failed),
                _ => Task.CompletedTask
            };

            await task;
        }

        private async Task<E_commerce.Domain.Entites.Baskets.CustomerBasket> GetBasketOrThrowAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            return basket ?? throw new BasketNotFoundException(basketId);
        }

        private async Task SynchronizeBasketPricesAsync(E_commerce.Domain.Entites.Baskets.CustomerBasket basket)
        {
            // Validate items against DB prices (Safety Check)
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<E_commerce.Domain.Entites.Products.Product, int>().GetByIdAsync(item.Id);
                if (product == null) throw new ProductNotFoundException(item.Id);

                // Sync the basket price with the real DB price
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }
        }

        private async Task<decimal> CalculateShippingPriceAsync(E_commerce.Domain.Entites.Baskets.CustomerBasket basket)
        {
            if (!basket.DeliveryMethodId.HasValue) return 0;

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod == null) throw new DeliveryMethodNotFoundException();

            basket.ShippingCost = (decimal)deliveryMethod.Price;
            return basket.ShippingCost.Value;
        }

        private long CalculateTotalInCents(E_commerce.Domain.Entites.Baskets.CustomerBasket basket, decimal shippingPrice)
        {
            var subTotal = basket.Items.Sum(i => i.Price * i.Quantity);
            return (long)Math.Round((subTotal + shippingPrice) * 100);
        }

        private async Task ApplyStripePaymentIntentAsync(E_commerce.Domain.Entites.Baskets.CustomerBasket basket, long amount)
        {
            StripeConfiguration.ApiKey = configuration["StripeOptions:SecertKey"];
            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var intent = await service.CreateAsync(createOptions);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
                return;
            }

            var updateOptions = new PaymentIntentUpdateOptions { Amount = amount };
            await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
        }

        private Event ConstructStripeEvent(string json, string header)
        {
            var endpointSecret = configuration.GetSection("StripeOptions")["EndPointSecert"];
            return EventUtility.ConstructEvent(json, header, endpointSecret);
        }

        private async Task UpdateOrderStatusAsync(string paymentIntentId, OrderStatus status)
        {
            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);

            if (order == null) throw new OrderNotFoundException();

            order.OrderStatus = status;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}