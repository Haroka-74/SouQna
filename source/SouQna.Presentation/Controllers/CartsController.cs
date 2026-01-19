using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.DTOs.Carts;
using SouQna.Application.Features.Carts.Queries.GetCart;
using SouQna.Application.Features.Carts.Commands.AddItemToCart;
using SouQna.Application.Features.Carts.Commands.RemoveItemFromCart;

namespace SouQna.Presentation.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController(ISender sender) : ControllerBase
    {
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(CartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCartAsync(Guid userId)
            => Ok(await sender.Send(new GetCartQuery(userId)));

        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItemToCartAsync(AddItemToCartCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpDelete("users/{userId:guid}/items/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveItemFromCartAsync(Guid userId, Guid productId)
        {
            await sender.Send(new RemoveItemFromCartCommand(userId, productId));
            return NoContent();
        }
    }
}