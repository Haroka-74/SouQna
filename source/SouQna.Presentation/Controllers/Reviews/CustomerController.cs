using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Reviews.Customer.GetReviews;
using SouQna.Application.Features.Reviews.Customer.CreateReview;

namespace SouQna.Presentation.Controllers.Reviews
{
    [Route("api/customer/products/{productId:guid}/reviews")]
    [ApiController]
    [Authorize]
    [Tags("Customer - Reviews")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetReviews(
            [FromRoute] Guid productId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            return Ok(
                await sender.Send(
                    new GetReviewsRequest(
                        productId,
                        pageNumber,
                        pageSize
                    )
                )
            );
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