using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Carts.Commands.RemoveItemFromCart
{
    public class RemoveItemFromCartCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveItemFromCartCommand>
    {
        public async Task Handle(
            RemoveItemFromCartCommand command,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == command.UserId,
                c => c.CartItems
            );

            if(cart is null)
                return;

            cart.RemoveItem(command.ProductId);
            await unitOfWork.SaveChangesAsync();
        }
    }
}