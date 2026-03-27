using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Reviews.Customer.GetReviews
{
    public record GetReviewsRequest(
        Guid ProductId,
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Page<DTOs.ReviewDTO>>;
}