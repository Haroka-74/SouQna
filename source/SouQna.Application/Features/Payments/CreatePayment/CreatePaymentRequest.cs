using MediatR;

namespace SouQna.Application.Features.Payments.CreatePayment
{
    public record CreatePaymentRequest(
        Guid UserId,
        Guid OrderId
    ) : IRequest<string>;
}