using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Presentation.API.Controllers
{
    public class AuthController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login(LoginRequestDto loginRequest)
        {
            var result = await serviceManager.AuthService.LoginAsync(loginRequest);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterRequestDto registerRequest)
        {
            var result = await serviceManager.AuthService.RegisterAsync(registerRequest);
            return Ok(result);
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.GetCurrentUserAddressAsync(email);
            return Ok(result);
        }

        [HttpPut("Address")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> UpdateCurrentUserAddress(UserAddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.UpdateCurrentUserAddressAsync(email, addressDto);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResponseDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.GetCurrentUserAsync(email);
            return Ok(result);
        }
        [HttpGet("emailexists")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            var result = await serviceManager.AuthService.CheckEmailExistsAsync(email);
            return Ok(result);
        }
    }
}
