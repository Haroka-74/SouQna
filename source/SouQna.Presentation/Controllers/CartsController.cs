using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Business.Contracts.Requests;

namespace SouQna.Presentation.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCartAsync()
            => Ok(await cartService.GetCartAsync(User.GetUserId()));

        [HttpPost("items")]
        [Authorize]
        public async Task<IActionResult> AddToCartAsync(AddToCartRequest request)
        {
            await cartService.AddToCartAsync(User.GetUserId(), request);
            return NoContent();
        }

        [HttpPatch("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItemAsync(Guid productId, UpdateCartItemRequest request)
        {
            await cartService.UpdateCartItemAsync(User.GetUserId(), productId, request);
            return NoContent();
        }

        [HttpDelete("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCartAsync(Guid productId)
        {
            await cartService.RemoveFromCartAsync(User.GetUserId(), productId);
            return NoContent();
        }
    }
}