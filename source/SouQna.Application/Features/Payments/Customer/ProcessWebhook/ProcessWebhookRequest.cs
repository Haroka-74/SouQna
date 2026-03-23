using MediatR;

namespace SouQna.Application.Features.Payments.Customer.ProcessWebhook
{
    public record ProcessWebhookRequest(
        string Json,
        string Hmac
    ) : IRequest;
}