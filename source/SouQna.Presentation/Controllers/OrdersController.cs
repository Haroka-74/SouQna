using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Orders.GetOrder;
using SouQna.Application.Features.Orders.ShipOrder;
using SouQna.Application.Features.Orders.GetOrders;
using SouQna.Application.Features.Orders.CreateOrder;
using SouQna.Application.Features.Orders.ProcessOrder;
using SouQna.Application.Features.Orders.DeliverOrder;
using SouQna.Application.Features.Orders.GetUserOrders;

namespace SouQna.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(ISender sender) : ControllerBase
    {
        [HttpGet("all")]
        [Authorize(Roles = "warehouse,delivery")]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] GetOrdersRequest request)
        {
            return Ok(await sender.Send(request));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrdersAsync([FromQuery] Contracts.GetUserOrdersRequest request)
        {
            return Ok(
                await sender.Send(
                    new GetUserOrdersRequest(
                        User.GetUserId(),
                        request.PageNumber,
                        request.PageSize,
                        request.OrderStatus
                    )
                )
            );
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOrderAsync(Guid id)
        {
            return Ok(
                await sender.Send(
                    new GetOrderRequest(
                        id,
                        User.GetUserId()
                    )
                )
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(Contracts.CreateOrderRequest request)
        {
            var result = await sender.Send(
                new CreateOrderRequest(
                    User.GetUserId(),
                    request.ShippingFullName,
                    request.ShippingPhoneNumber,
                    request.ShippingCity,
                    request.ShippingAddressLine
                )
            );
            return CreatedAtAction(
                "GetOrder",
                new { id = result.Id },
                result
            );
        }

        [HttpPost("warehouse/{id:guid}/process")]
        [Authorize(Roles = "warehouse")]
        public async Task<IActionResult> ProcessOrderAsync(Guid id)
        {
            await sender.Send(new ProcessOrderRequest(id));
            return NoContent();
        }

        [HttpPost("warehouse/{id:guid}/ship")]
        [Authorize(Roles = "warehouse")]
        public async Task<IActionResult> ShipOrderAsync(Guid id)
        {
            await sender.Send(new ShipOrderRequest(id));
            return NoContent();
        }

        [HttpPost("delivery/{id:guid}/deliver")]
        [Authorize(Roles = "delivery")]
        public async Task<IActionResult> DeliverOrderAsync(Guid id)
        {
            await sender.Send(new DeliverOrderRequest(id));
            return NoContent();
        }

        [HttpGet("/api/warehouse/orders/{id}")]
        [Authorize(Roles = "warehouse,delivery")]
        public async Task<IActionResult> GetForStaff(Guid id)
        {
            return Ok(
                await sender.Send(
                    new GetOrderRequest(
                        id,
                        null
                    )
                )
            );
        }
    }
}