using E_commerce.Domain.Entites.Identity;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_commerce.Domain.Exceptions.UnAuthorized;
using E_Commerce.Serviece.Abstraction.Auth;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using E_Commerce.Shared;

namespace E_Commerce.Service.Auth
{
    public class AuthService(UserManager<AppUser> userManager,IOptions<JwtOptions> options) : IAuthService
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
                Token = await GenerateJWTTokenAsync(user)
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
                Token = await GenerateJWTTokenAsync(user)
            };
        }

        private async Task<string> GenerateJWTTokenAsync(AppUser user)
        {
            //1.Header(Algo+type)
            //2.Claims
            //3.Signatre

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtOptions = options.Value;
            var byteSecurityKey = Encoding.UTF8.GetBytes(jwtOptions.SecurityKey);
            var key = new SymmetricSecurityKey(byteSecurityKey);

            var token = new JwtSecurityToken(
                claims: authClaims,
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: DateTime.Now.AddDays(jwtOptions.DurationDays),
                signingCredentials : new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
