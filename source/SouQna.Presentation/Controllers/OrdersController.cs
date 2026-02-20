using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Business.Contracts.Requests.Orders;

namespace SouQna.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrdersAsync([FromQuery] GetOrdersRequest request)
            => Ok(await orderService.GetUserOrdersAsync(User.GetUserId(), request));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
            => CreatedAtAction("CreateOrder", await orderService.CreateOrderAsync(User.GetUserId(), request)); // create get order by id and refer to it
    }
}