namespace SouQna.Application.Features.Payments.Customer.CreatePayment.DTOs
{
    public record PaymentItemDTO(
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal
    );
}