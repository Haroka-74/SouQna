using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Authentication.Register;

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
    }
}