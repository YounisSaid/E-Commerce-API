using AutoMapper;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name));

        }
    }
}
