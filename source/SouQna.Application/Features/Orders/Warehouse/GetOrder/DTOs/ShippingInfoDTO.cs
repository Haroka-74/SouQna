namespace SouQna.Application.Features.Orders.Warehouse.GetOrder.DTOs
{
    public record ShippingInfoDTO(
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    );
}