using E_commerce.Domain.Entites.Identity;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_commerce.Domain.Exceptions.UnAuthorized;
using E_Commerce.Serviece.Abstraction.Auth;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Auth
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user is null) throw new UserNotFoundException(loginRequestDto.Email);

            var isPasswordVaild = await userManager.CheckPasswordAsync(user,loginRequestDto.Password);
            if (!isPasswordVaild) throw new UnAuthorizedException();

            return new UserResponseDto
            { 
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token ="TODO"
            };

        }

        public async Task<UserResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var user = new AppUser
            { 
                Email = registerRequestDto.Email,
                DisplayName = registerRequestDto.DisplayName,
                UserName = registerRequestDto.UserName,
                PhoneNumber = registerRequestDto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded)
                throw new RegesiterationBadRequestException(result.Errors.Select(E => E.Description).ToList());

            return new UserResponseDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "TODO"
            };
        }
    }
}
