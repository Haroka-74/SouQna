using MediatR;
using SouQna.Domain.Entities;
using SouQna.Domain.Exceptions;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Payments.Customer.CreatePayment
{
    public class CreatePaymentRequestHandler(
        IUnitOfWork unitOfWork,
        IPaymentService paymentService
    ) : IRequestHandler<CreatePaymentRequest, string>
    {
        public async Task<string> Handle(
            CreatePaymentRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.OrderId && o.UserId == request.UserId,
                o => o.User,
                o => o.OrderItems,
                o => o.Payments
            ) ?? throw new NotFoundException($"Order with (id: {request.OrderId}) was not found");

            if(!order.IsPending)
                throw new InvalidStateException($"Cannot pay, order status is {order.OrderStatus}");

            var latestPayment = order.Payments.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            if(
                latestPayment is not null &&
                latestPayment.IsPending &&
                !latestPayment.IsExhausted &&
                !latestPayment.IsExpired
            )
            {
                return latestPayment.CheckoutUrl;
            }

            if(
                latestPayment is not null &&
                latestPayment.IsPending &&
                latestPayment.IsExpired
            )
            {
                latestPayment.MarkAsExpired();
            }

            var items = order.OrderItems.Select(
                i => new DTOs.PaymentItemDTO(
                    i.ItemName,
                    i.ItemImage,
                    i.ItemPrice,
                    i.ItemQuantity,
                    i.ItemPrice * i.ItemQuantity
                )
            ).ToList();

            var (IntentionOrderId, CreatedAt, CheckoutUrl) = await paymentService.CreateIntentionAsync(
                order.User.Email,
                order.Id,
                order.Total,
                order.ShippingFullName,
                order.ShippingPhoneNumber,
                order.ShippingCity,
                order.ShippingAddressLine,
                items
            );

            var payment = Payment.Create(
                order.Id,
                IntentionOrderId,
                CheckoutUrl,
                CreatedAt
            );

            await unitOfWork.Payments.AddAsync(payment);
            await unitOfWork.SaveChangesAsync();

            return CheckoutUrl;
        }
    }
}