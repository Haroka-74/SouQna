using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Reviews.Customer.CreateReview;

namespace SouQna.Presentation.Controllers.Reviews
{
    [Route("api/products/{productId:guid}/reviews")]
    [ApiController]
    [Authorize]
    [Tags("Customer - Reviews")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReviews()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Guid productId, Contracts.Reviews.CreateReviewRequest request)
        {
            var id = await sender.Send(
                new CreateReviewRequest(
                    User.GetUserId(),
                    productId,
                    request.Rating,
                    request.Body

                )
            );

            return CreatedAtAction(nameof(GetReviews), new { productId }, new { id });
        }
    }
}