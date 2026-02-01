using AutoMapper;
using E_commerce.Domain.Entites.Identity;
using E_Commerce.Shared.Dtos.Auth;

namespace E_Commerce.Service.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserAddressDto,Address>().ReverseMap();
        }

       
    }
}
