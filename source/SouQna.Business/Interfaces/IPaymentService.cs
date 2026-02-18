namespace SouQna.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(Guid orderId);
    }
}