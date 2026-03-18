using MediatR;
using SouQna.Application.Features.Orders.Shared;

namespace SouQna.Application.Features.Orders.CreateOrder
{
    public record CreateOrderRequest(
        Guid UserId,
        string ShippingFullName,
        string ShippingPhoneNumber,
        string ShippingCity,
        string ShippingAddressLine
    ) : IRequest<OrderDTO>;
}