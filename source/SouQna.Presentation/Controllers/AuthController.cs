using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Authentication.Commands.Login;
using SouQna.Application.Features.Authentication.Commands.Register;
using SouQna.Application.Features.Authentication.Commands.RevokeToken;
using SouQna.Application.Features.Authentication.Commands.ConfirmEmail;
using SouQna.Application.Features.Authentication.Commands.RefreshToken;
using SouQna.Application.Features.Authentication.Commands.ResendEmailConfirmation;

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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
            => Ok(await sender.Send(command));

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
        {
            await sender.Send(command);
            return NoContent();
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmationAsync(ResendEmailConfirmationCommand command)
        {
            await sender.Send(command);
            return Ok();
        }
    }
}