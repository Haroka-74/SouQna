using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Payments.ProcessWebhook
{
    public class ProcessWebhookRequestHandler(
        IUnitOfWork unitOfWork,
        IHmacVerifier hmacVerifier,
        IWebhookParser webhookParser
    ) : IRequestHandler<ProcessWebhookRequest>
    {
        public async Task Handle(
            ProcessWebhookRequest request,
            CancellationToken cancellationToken
        )
        {
            if(!hmacVerifier.Verify(request.Json, request.Hmac))
                throw new UnauthorizedException("Invalid HMAC");

            var (Success, OrderId, IntentionOrderId) = webhookParser.Parse(request.Json);

            var payment = await unitOfWork.Payments.FindAsync(
                p => p.IntentionOrderId == IntentionOrderId,
                p => p.Order
            );

            if (payment is null || !payment.IsPending)
                return;

            if(Success)
            {
                payment.MarkAsPaid();
                payment.Order.Confirm();

                var cart = await unitOfWork.Carts.FindAsync(
                    c => c.UserId == payment.Order.UserId,
                    c => c.CartItems
                );

                if(cart is not null && cart.CartItems.Count != 0)
                    await unitOfWork.CartItems.DeleteRangeAsync(cart.CartItems);
            }
            else
            {
                payment.RegisterFailedAttempt();
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}