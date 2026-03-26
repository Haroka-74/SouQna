using MediatR;
using SouQna.Domain.Enums;
using SouQna.Domain.Entities;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Reviews.Customer.CreateReview
{
    public class CreateReviewRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateReviewRequest, Guid>
    {
        public async Task<Guid> Handle(
            CreateReviewRequest request,
            CancellationToken cancellationToken
        )
        {
            if(!await unitOfWork.Products.AnyAsync(p => p.Id == request.ProductId))
                throw new NotFoundException($"Product with (id: {request.ProductId}) was not found");

            var hasPurchased = await unitOfWork.Orders.AnyAsync(
                o =>
                    o.UserId == request.UserId &&
                    o.OrderStatus == OrderStatus.Delivered &&
                    o.OrderItems.Any(
                        oi => oi.ProductId == request.ProductId
                    )
            );

            if(!hasPurchased)
                throw new ForbiddenException("You can only review products from your delivered orders");

            var existing = await unitOfWork.Reviews.FindAsync(
                r => r.UserId == request.UserId && r.ProductId == request.ProductId
            );

            if(existing is not null)
                throw new ConflictException("You have already reviewed this product");

            var review = Review.Create(
                request.UserId,
                request.ProductId,
                request.Rating,
                request.Body
            );

            await unitOfWork.Reviews.AddAsync(review);
            await unitOfWork.SaveChangesAsync();

            return review.Id;
        }
    }
}