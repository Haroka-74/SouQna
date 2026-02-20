using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SouQna.Business.Contracts.Requests.Authentication;

namespace SouQna.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
            => Ok(await authService.RegisterAsync(request));

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
            => Ok(await authService.LoginAsync(request));
    }
}