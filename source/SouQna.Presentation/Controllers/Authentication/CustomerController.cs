using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Authentication.Customer.LoginUser;
using SouQna.Application.Features.Authentication.Customer.RegisterUser;

namespace SouQna.Presentation.Controllers.Authentication
{
    [Route("api/customer/auth")]
    [ApiController]
    [AllowAnonymous]
    [Tags("Customer - Authentication")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
            => Ok(await sender.Send(request));

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserRequest request)
            => Ok(await sender.Send(request));
    }
}