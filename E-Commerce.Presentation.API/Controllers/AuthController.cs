using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    public class AuthController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var result = await serviceManager.AuthService.LoginAsync(loginRequest);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var result = await serviceManager.AuthService.RegisterAsync(registerRequest);
            return Ok(result);
        }
    }
}
