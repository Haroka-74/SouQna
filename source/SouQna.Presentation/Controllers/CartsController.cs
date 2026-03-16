using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Carts.GetCart;
using SouQna.Application.Features.Carts.AddToCart;
using SouQna.Application.Features.Carts.UpdateCartItem;
using SouQna.Application.Features.Carts.RemoveFromCart;

namespace SouQna.Presentation.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCartAsync()
            => Ok(await sender.Send(new GetCartRequest(User.GetUserId())));

        [HttpPost("items")]
        [Authorize]
        public async Task<IActionResult> AddToCartAsync(Contracts.AddToCartRequest request)
        {
            return Ok(
                await sender.Send(
                    new AddToCartRequest(
                        User.GetUserId(),
                        request.ProductId,
                        request.Quantity
                    )
                )
            );
        }

        [HttpPut("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItemAsync(Guid productId, [FromBody] int quantity)
        {
            return Ok(
                await sender.Send(
                    new UpdateCartItemRequest(
                        User.GetUserId(),
                        productId,
                        quantity
                    )
                )
            );
        }

        [HttpDelete("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCartAsync(Guid productId)
        {
            return Ok(
                await sender.Send(
                    new RemoveFromCartRequest(
                        User.GetUserId(),
                        productId
                    )
                )
            );
        }
    }
}