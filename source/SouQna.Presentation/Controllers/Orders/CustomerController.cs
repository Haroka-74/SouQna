using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Orders.Customer.GetOrder;
using SouQna.Application.Features.Orders.Customer.GetOrders;
using SouQna.Application.Features.Orders.Customer.CreateOrder;

namespace SouQna.Presentation.Controllers.Orders
{
    [Route("api/customer/orders")]
    [ApiController]
    [Authorize]
    [Tags("Customer - Orders")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] Contracts.Orders.GetOrdersRequest request)
        {
            return Ok(
                await sender.Send(
                    new GetOrdersRequest(
                        User.GetUserId(),
                        request.PageNumber,
                        request.PageSize,
                        request.OrderStatus
                    )
                )
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
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
        public async Task<IActionResult> CreateOrder(Contracts.Orders.CreateOrderRequest request)
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
                nameof(GetOrder),
                new { id = result.Id },
                result
            );
        }
    }
}