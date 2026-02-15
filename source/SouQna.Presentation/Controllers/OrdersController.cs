using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SouQna.Business.Contracts.Requests;

namespace SouQna.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrdersAsync([FromQuery] GetOrdersRequest request)
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            return Ok(await orderService.GetUserOrdersAsync(userId, request));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
        {
            _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            return CreatedAtAction("CreateOrder", await orderService.CreateOrderAsync(userId, request));
        }
    }
}