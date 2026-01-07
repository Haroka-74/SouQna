using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Authentication.Register;
using SouQna.Application.Features.Authentication.ConfirmEmail;

namespace SouQna.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await sender.Send(request));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            await sender.Send(new ConfirmEmailRequest(token));
            return Ok("Email confirmed successfully");
        }
    }
}