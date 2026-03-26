using MediatR;

namespace SouQna.Application.Features.Reviews.Customer.CreateReview
{
    public record CreateReviewRequest(
        Guid UserId,
        Guid ProductId,
        int Rating,
        string Body
    ) : IRequest<Guid>;
}