namespace SouQna.Presentation.Contracts
{
    public record CreateOrderRequest(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}