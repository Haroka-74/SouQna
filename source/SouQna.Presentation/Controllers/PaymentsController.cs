using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SouQna.Presentation.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {
        [HttpPost("{orderId:guid}/intent")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntentAsync(Guid orderId)
            => Ok(new { checkoutUrl = await paymentService.CreatePaymentIntentAsync(orderId) });
    }
}
