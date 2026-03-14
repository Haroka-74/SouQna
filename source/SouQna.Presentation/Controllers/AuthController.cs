using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Authentication.LoginUser;
using SouQna.Application.Features.Authentication.RegisterUser;

namespace SouQna.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterUserRequest request)
            => Ok(await sender.Send(request));

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginUserRequest request)
            => Ok(await sender.Send(request));
    }
}