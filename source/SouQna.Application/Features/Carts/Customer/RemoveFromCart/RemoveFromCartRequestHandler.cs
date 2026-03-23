using MediatR;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Carts.Customer.RemoveFromCart
{
    public class RemoveFromCartRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveFromCartRequest, DTOs.CartDTO>
    {
        public async Task<DTOs.CartDTO> Handle(
            RemoveFromCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            ) ?? throw new NotFoundException($"Cart for user (id: {request.UserId}) was not found");

            cart.RemoveItem(request.ProductId);
            await unitOfWork.SaveChangesAsync();

            var items = cart.CartItems.Select(
                i => new DTOs.CartItemDTO(
                    i.Id,
                    i.ProductId,
                    i.Product.Name,
                    i.Product.Image,
                    i.Quantity,
                    i.PriceSnapshot,
                    i.Quantity * i.PriceSnapshot
                )
            ).ToList();

            return new DTOs.CartDTO(
                items.Sum(i => i.Quantity),
                items.Sum(i => i.Subtotal),
                items.AsReadOnly()
            );
        }
    }
}