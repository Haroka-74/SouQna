using MediatR;

namespace SouQna.Application.Features.Orders.Customer.CreateOrder
{
    public record CreateOrderRequest(
        Guid UserId,
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    ) : IRequest<DTOs.OrderDTO>;
}