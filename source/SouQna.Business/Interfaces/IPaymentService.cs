namespace SouQna.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(Guid orderId);
        Task ProcessPaymentWebhookAsync(string json, string hmac);
    }
}