using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Carts.Customer.GetCart
{
    public class GetCartRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCartRequest, DTOs.CartDTO>
    {
        public async Task<DTOs.CartDTO> Handle(
            GetCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            );

            if(cart is null)
                return new DTOs.CartDTO(0, 0m, []);

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