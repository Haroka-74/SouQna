using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}