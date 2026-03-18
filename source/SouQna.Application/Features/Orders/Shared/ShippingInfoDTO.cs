namespace SouQna.Application.Features.Orders.Shared
{
    public record ShippingInfoDTO(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}