using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_commerce.Domain.Exceptions.BadRequest;
using E_commerce.Domain.Exceptions.NotFound;
using E_commerce.Domain.Exceptions.UnAuthorized;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Service.Services.Auth
{
    public class AuthService(
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> options,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IAuthService
    {
        private readonly JwtOptions _jwtOptions = options.Value;

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }

        public async Task<UserResponseDto?> GetCurrentUserAsync(string email)
        {
            var user = await FindUserByEmailOrThrowAsync(email);
            return await MapToUserResponseDtoAsync(user);
        }

        public async Task<UserAddressDto?> GetCurrentUserAddressAsync(string email)
        {
            var user = await FindUserWithAddressOrThrowAsync(email);

            if (user.Address is null)
                throw new AddressNotFoundException(email);

            return mapper.Map<UserAddressDto>(user.Address);
        }

        public async Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await FindUserByEmailOrThrowAsync(loginRequestDto.Email);

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isPasswordValid)
                throw new UnAuthorizedException();

            return await MapToUserResponseDtoAsync(user);
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
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new RegesiterationBadRequestException(errors);
            }

            return await MapToUserResponseDtoAsync(user);
        }

        public async Task<UserAddressDto?> UpdateCurrentUserAddressAsync(string email, UserAddressDto addressDto)
        {
            var user = await FindUserWithAddressOrThrowAsync(email);

            UpdateOrAssignAddress(user, addressDto);

            await userManager.UpdateAsync(user);
            return mapper.Map<UserAddressDto>(user.Address);
        }

        private async Task<AppUser> FindUserByEmailOrThrowAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user ?? throw new UserNotFoundException(email);
        }

        private async Task<AppUser> FindUserWithAddressOrThrowAsync(string email)
        {
            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);

            return user ?? throw new UserNotFoundException(email);
        }

        private void UpdateOrAssignAddress(AppUser user, UserAddressDto addressDto)
        {
            if (user.Address is not null)
            {
                mapper.Map(addressDto, user.Address);
                return;
            }

            user.Address = mapper.Map<Address>(addressDto);
        }

        private async Task<UserResponseDto> MapToUserResponseDtoAsync(AppUser user)
        {
            return new UserResponseDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };
        }

        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            var authClaims = await GetUserClaimsAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(_jwtOptions.DurationDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<List<Claim>> GetUserClaimsAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                new(ClaimTypes.GivenName, user.DisplayName)
            };

            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}