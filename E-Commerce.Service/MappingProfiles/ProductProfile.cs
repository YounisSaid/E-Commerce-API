using AutoMapper;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Shared.Dtos.Products;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PicutreURL, opt => opt.MapFrom(new ProductPictureResolver(configuration)));

        }
    }

    public class ProductPictureResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string?>
    {
        public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PicutreURL))
                return string.Empty;
            return $"{configuration["BaseURL"]}{source.PicutreURL}";
        }
    }
}
