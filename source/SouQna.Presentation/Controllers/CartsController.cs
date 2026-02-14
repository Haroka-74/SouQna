using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
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
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            return Ok(await cartService.GetCartAsync(userId));
        }

        [HttpPost("items")]
        [Authorize]
        public async Task<IActionResult> AddToCartAsync(AddToCartRequest request)
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            await cartService.AddToCartAsync(userId, request);
            return NoContent();
        }

        [HttpPatch("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItemAsync(Guid productId, UpdateCartItemRequest request)
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            await cartService.UpdateCartItemAsync(userId, productId, request);
            return NoContent();
        }

        [HttpDelete("items/{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCartAsync(Guid productId)
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            await cartService.RemoveFromCartAsync(userId, productId);
            return NoContent();
        }
    }
}