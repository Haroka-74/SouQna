using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Payments.Customer.CreatePayment;

namespace SouQna.Presentation.Controllers.Payments
{
    [Route("api/customer/payments")]
    [ApiController]
    [Authorize]
    [Tags("Customer - Payments")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpPost("{orderId:guid}/intent")]
        public async Task<IActionResult> CreatePayment(Guid orderId)
        {
            var url = await sender.Send(
                new CreatePaymentRequest(
                    User.GetUserId(),
                    orderId
                )
            );

            return Ok(new { url });
        }
    }
}