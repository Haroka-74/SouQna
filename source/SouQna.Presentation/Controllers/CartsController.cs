using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Carts.Commands.AddItemToCart;

namespace SouQna.Presentation.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItemToCartAsync(AddItemToCartCommand command)
        {
            await sender.Send(command);
            return Ok();
        }
    }
}