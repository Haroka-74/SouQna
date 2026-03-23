using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Orders.Delivery.DeliverOrder;
using SouQna.Application.Features.Orders.Delivery.GetShippedOrders;

namespace SouQna.Presentation.Controllers.Orders
{
    [Route("api/delivery/orders")]
    [ApiController]
    [Authorize(Roles = "delivery")]
    [Tags("Delivery - Orders")]
    public class DeliveryController(ISender sender) : ControllerBase
    {
        [HttpGet("shipped")]
        public async Task<IActionResult> GetShippedOrders([FromQuery] GetShippedOrdersRequest request)
            => Ok(await sender.Send(request));

        [HttpPost("{id:guid}/deliver")]
        public async Task<IActionResult> DeliverOrder(Guid id)
        {
            await sender.Send(new DeliverOrderRequest(id));
            return NoContent();
        }
    }
}