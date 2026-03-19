using SouQna.Application.DTOs.Orders;

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
            List<OrderItemDTO> orderItems
        );
    }
}