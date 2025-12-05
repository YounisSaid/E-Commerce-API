using AutoMapper;
using E_commerce.Domain.Entites.Orders;
using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddressDto,ShippingAddress>().ReverseMap();
            CreateMap<ProductInOrderItemDto,ProductInOrderItem>().ReverseMap();
            CreateMap<Order,OrderResponseDto>()
                .ForMember(d => d.DeliveryMethod,o => o.MapFrom(src=>src.DeliveryMethod.ShortName))
                .ForMember(d=>d.Total,o=>o.MapFrom(src => src.GetTotal()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(p => p.ProductId, o => o.MapFrom(src => src.Product.ProductId))
                .ForMember(p => p.ProductName, o => o.MapFrom(src => src.Product.ProductName))
                .ForMember(p => p.Quantity, o => o.MapFrom(src => src.Product.Quantity));
        }

       
    }
}
