using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Baskets;
using E_commerce.Domain.Entites.Orders;
using E_commerce.Domain.Entites.Products;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications.Orders;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Service.Services.Orders
{
    public class OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto request, string email)
        {
            var deliveryMethod = await GetDeliveryMethodOrThrowAsync(request.DeliveryMethodId);
            var basket = await GetBasketOrThrowAsync(request.BasketId);

            var orderItems = await CreateOrderItemsFromBasketAsync(basket);
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            await RemoveExistingOrderWithPaymentIntent(basket.PaymentIntentId);

            var shippingAddress = mapper.Map<ShippingAddress>(request.OrderAddressDto);
            var order = new Order(email, shippingAddress, deliveryMethod, orderItems, (double)subTotal, basket.PaymentIntentId);

            unitOfWork.GetRepository<Order, Guid>().Add(order);

            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0) throw new CreateOrUpdateBadRequestException();

            return mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponseDto>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResponseDto>>(deliveryMethods);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersForSpecificUserAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            return mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderForSpecificUserByIdAsync(Guid id, string email)
        {
            var spec = new OrderSpecification(id, email);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);

            if (order is null) throw new OrderNotFoundException();

            return mapper.Map<OrderResponseDto>(order);
        }

        private async Task<DeliveryMethod> GetDeliveryMethodOrThrowAsync(int deliveryMethodId)
        {
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(deliveryMethodId);
            return deliveryMethod ?? throw new DeliveryMethodNotFoundException();
        }

        private async Task<CustomerBasket> GetBasketOrThrowAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            return basket ?? throw new BasketNotFoundException(basketId);
        }

        private async Task<List<OrderItem>> CreateOrderItemsFromBasketAsync(CustomerBasket basket)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                // Ensure the price used is the current price from the database, not the potentially stale basket price
                var price = product.Price;
                var productSnapshot = new ProductInOrderItem(item.Id, item.ProductName, item.PicutreURL);

                orderItems.Add(new OrderItem(productSnapshot, price, item.Quantity));
            }

            return orderItems;
        }

        private async Task RemoveExistingOrderWithPaymentIntent(string paymentIntentId)
        {
            if (string.IsNullOrEmpty(paymentIntentId)) return;

            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var existingOrder = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);

            if (existingOrder is not null)
            {
                unitOfWork.GetRepository<Order, Guid>().Remove(existingOrder);
            }
        }
    }
}