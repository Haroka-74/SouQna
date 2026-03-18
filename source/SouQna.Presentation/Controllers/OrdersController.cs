using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Orders.GetOrder;
using SouQna.Application.Features.Orders.CreateOrder;
using SouQna.Application.Features.Orders.GetUserOrders;

namespace SouQna.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(ISender sender) : ControllerBase
    {
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
                        User.GetUserId(),
                        id
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
    }
}