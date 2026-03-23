namespace SouQna.Presentation.Contracts.Orders
{
    public record CreateOrderRequest(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}