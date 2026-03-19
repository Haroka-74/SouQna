using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Payments.CreatePayment;
using SouQna.Application.Features.Payments.ProcessWebhook;

namespace SouQna.Presentation.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController(ISender sender) : ControllerBase
    {
        [HttpPost("{orderId:guid}/intent")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentAsync(Guid orderId)
        {
            var url = await sender.Send(
                new CreatePaymentRequest(
                    User.GetUserId(),
                    orderId
                )
            );

            return Ok(new { url });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> ProcessWebhookAsync([FromQuery] string hmac)
        {
            await sender.Send(
                new ProcessWebhookRequest(
                    await new StreamReader(Request.Body).ReadToEndAsync(),
                    hmac
                )
            );

            return Ok();
        }
    }
}