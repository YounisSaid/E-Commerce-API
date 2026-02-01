using E_Commerce.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Serviece.Abstraction
{
    public interface IAuthService
    {
        Task<UserResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<UserResponseDto?> GetCurrentUserAsync(string email);
        Task<UserAddressDto?> GetCurrentUserAddressAsync(string email);
        Task<UserAddressDto?> UpdateCurrentUserAddressAsync(string email, UserAddressDto addressDto);

    }
}
