namespace SouQna.Application.Features.Orders.Customer.GetOrder.DTOs
{
    public record ShippingInfoDTO(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}