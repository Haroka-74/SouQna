using MediatR;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Reviews.Customer.GetReviews
{
    public class GetReviewsRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetReviewsRequest, Page<DTOs.ReviewDTO>>
    {
        public async Task<Page<DTOs.ReviewDTO>> Handle(
            GetReviewsRequest request,
            CancellationToken cancellationToken
        )
        {
            var (items, totalCount) = await unitOfWork.Reviews.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                r => r.ProductId == request.ProductId,
                q => q.OrderByDescending(r => r.CreatedAt),
                r => r.User
            );

            var reviews = items.Select(
                r => new DTOs.ReviewDTO(
                    $"{r.User.FirstName} {r.User.LastName}",
                    r.Rating,
                    r.Body,
                    r.CreatedAt
                )
            ).ToList();

            return new Page<DTOs.ReviewDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                reviews.AsReadOnly()
            );
        }
    }
}