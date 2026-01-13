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
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterAsync(RegisterCommand command)
            => Ok(await sender.Send(command));

        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string token)
        {
            await sender.Send(new ConfirmEmailCommand(token));
            return Ok(new { message = "Email confirmed successfully" });
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
            => Ok(await sender.Send(command));

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
            => Ok(await sender.Send(command));

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
        {
            await sender.Send(command);
            return NoContent();
        }

        [HttpPost("resend-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendConfirmationAsync(ResendEmailConfirmationCommand command)
        {
            await sender.Send(command);
            return Ok();
        }
    }
}