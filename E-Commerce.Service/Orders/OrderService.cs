using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Orders;
using E_commerce.Domain.Entites.Products;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications.Orders;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Service.Orders
{
    public class OrderService(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto request, string email)
        {
            // Get ShippingAddress
            var orderAddress = mapper.Map<ShippingAddress>(request.OrderAddressDto);

            // Get deliveryMethod by ID
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException();

            // Get Basket By Id
            var basket = await basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException();

            var orderItemList = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                // check Price
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;

                var productInItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInItem, item.Price, item.Quantity);
                orderItemList.Add(orderItem);
            }
            // Get Total Price
            var subTotal = orderItemList.Sum(oi => oi.Price * oi.Quantity);
            // 1. create Order
            var order = new Order(email,orderAddress, deliveryMethod, orderItemList, (double)subTotal);

            //2.Add Order
            unitOfWork.GetRepository<Order, Guid>().Add(order);
            //3.Save Order
            var saveResult = await unitOfWork.SaveChangesAsync();
            if (saveResult < 0) throw new CreateOrUpdateBadRequestException();

            return mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponseDto>> GetAllDeliveryMethodsAsync()
        {
           var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethodResponseDto>>(deliveryMethods);

        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersForSpecificUserAsync(string email)
        {
            var specs = new OrderSpecification(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAsync(specs);
            return mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderForSpecificUserByIdAsync(Guid Id, string email)
        {
            var specs = new OrderSpecification(Id, email);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(specs);
            return mapper.Map<OrderResponseDto>(order);
        }
    }
}
