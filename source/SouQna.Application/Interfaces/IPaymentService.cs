using SouQna.Application.Features.Payments.Customer.CreatePayment.DTOs;

namespace SouQna.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<(long IntentionOrderId, DateTime CreatedAt, string CheckoutUrl)> CreateIntentionAsync(
            string userEmail,
            Guid orderId,
            decimal total,
            string shippingFullName,
            string shippingPhoneNumber,
            string shippingCity,
            string shippingAddressLine,
            List<PaymentItemDTO> paymentItems
        );
    }
}