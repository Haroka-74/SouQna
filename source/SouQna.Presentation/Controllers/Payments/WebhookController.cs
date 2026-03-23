using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Payments.Customer.ProcessWebhook;

namespace SouQna.Presentation.Controllers.Payments
{
    [Route("api/webhooks/payments")]
    [ApiController]
    [Tags("Webhooks - Payments")]
    public class WebhookController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessWebhook([FromQuery] string hmac)
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