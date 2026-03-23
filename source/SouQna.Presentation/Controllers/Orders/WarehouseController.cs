using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Orders.Warehouse.GetOrder;
using SouQna.Application.Features.Orders.Warehouse.ShipOrder;
using SouQna.Application.Features.Orders.Warehouse.ProcessOrder;
using SouQna.Application.Features.Orders.Warehouse.GetConfirmedOrders;
using SouQna.Application.Features.Orders.Warehouse.GetProcessingOrders;

namespace SouQna.Presentation.Controllers.Orders
{
    [Route("api/warehouse/orders")]
    [ApiController]
    [Authorize(Roles = "warehouse")]
    [Tags("Warehouse - Orders")]
    public class WarehouseController(ISender sender) : ControllerBase
    {
        [HttpGet("confirmed")]
        public async Task<IActionResult> GetConfirmedOrders([FromQuery] GetConfirmedOrdersRequest request)
            => Ok(await sender.Send(request));

        [HttpGet("processing")]
        public async Task<IActionResult> GetProcessingOrders([FromQuery] GetProcessingOrdersRequest request)
            => Ok(await sender.Send(request));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            return Ok(
                await sender.Send(
                    new GetOrderRequest(id)
                )
            );
        }

        [HttpPost("{id:guid}/process")]
        public async Task<IActionResult> ProcessOrder(Guid id)
        {
            await sender.Send(new ProcessOrderRequest(id));
            return NoContent();
        }

        [HttpPost("{id:guid}/ship")]
        public async Task<IActionResult> ShipOrder(Guid id)
        {
            await sender.Send(new ShipOrderRequest(id));
            return NoContent();
        }
    }
}