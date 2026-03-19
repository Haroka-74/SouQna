namespace SouQna.Application.DTOs.Orders
{
    public record ShippingInfoDTO(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}