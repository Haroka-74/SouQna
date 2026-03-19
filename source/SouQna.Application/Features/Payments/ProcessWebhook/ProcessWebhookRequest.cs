using MediatR;

namespace SouQna.Application.Features.Payments.ProcessWebhook
{
    public record ProcessWebhookRequest(
        string Json,
        string Hmac
    ) : IRequest;
}