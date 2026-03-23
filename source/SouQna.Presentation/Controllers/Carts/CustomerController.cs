using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Carts.Customer.GetCart;
using SouQna.Application.Features.Carts.Customer.AddToCart;
using SouQna.Application.Features.Carts.Customer.UpdateCartItem;
using SouQna.Application.Features.Carts.Customer.RemoveFromCart;

namespace SouQna.Presentation.Controllers.Carts
{
    [Route("api/customer/carts")]
    [ApiController]
    [Authorize]
    [Tags("Customer - Carts")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
            => Ok(await sender.Send(new GetCartRequest(User.GetUserId())));

        [HttpPost("items")]
        public async Task<IActionResult> AddToCart(Contracts.Carts.AddToCartRequest request)
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
        public async Task<IActionResult> UpdateCartItem(Guid productId, Contracts.Carts.UpdateCartItemRequest request)
        {
            return Ok(
                await sender.Send(
                    new UpdateCartItemRequest(
                        User.GetUserId(),
                        productId,
                        request.Quantity
                    )
                )
            );
        }

        [HttpDelete("items/{productId:guid}")]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
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