using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Authentication.Commands.Login;
using SouQna.Application.Features.Authentication.Commands.Register;
using SouQna.Application.Features.Authentication.Commands.ConfirmEmail;

namespace SouQna.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterCommand command)
            => Ok(await sender.Send(command));

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string token)
        {
            await sender.Send(new ConfirmEmailCommand(token));
            return Ok("Email confirmed successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
            => Ok(await sender.Send(command));

        [HttpGet("get-data"), Authorize]
        public async Task<IActionResult> GetData()
        {
            return Ok("Hello from secured controller");
        }
    }
}