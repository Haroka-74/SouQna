using MediatR;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Carts.Customer.UpdateCartItem
{
    public class UpdateCartItemRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCartItemRequest, DTOs.CartDTO>
    {
        public async Task<DTOs.CartDTO> Handle(
            UpdateCartItemRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            ) ?? throw new NotFoundException($"Cart for user (id: {request.UserId}) was not found");

            cart.UpdateItemQuantity(request.ProductId, request.Quantity);
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