using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Carts.Commands.UpdateCartItemQuantity
{
    public class UpdateCartItemQuantityCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCartItemQuantityCommand>
    {
        public async Task Handle(
            UpdateCartItemQuantityCommand command,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == command.UserId,
                c => c.CartItems
            );

            if(cart is null)
                return;

            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.ProductId
            );

            if(product is null)
                return;

            if(!product.HasSufficientStock(command.Quantity))
                return;

            cart.UpdateItemQuantity(command.ProductId, command.Quantity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}