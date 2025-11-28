using E_Commerce.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Serviece.Abstraction.Auth
{
    public interface IAuthService
    {
        Task<UserResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
