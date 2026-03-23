using MediatR;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Orders.Warehouse.GetOrder
{
    public class GetOrderRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetOrderRequest, DTOs.OrderDTO>
    {
        public async Task<DTOs.OrderDTO> Handle(
            GetOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.OrderId,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {request.OrderId}) was not found");

            var items = order.OrderItems.Select(
                i => new DTOs.OrderItemDTO(
                    i.ItemName,
                    i.ItemImage,
                    i.ItemPrice,
                    i.ItemQuantity,
                    i.ItemQuantity * i.ItemPrice
                )
            ).ToList();

            return new DTOs.OrderDTO(
                order.OrderNumber,
                new DTOs.ShippingInfoDTO(
                    order.ShippingFullName,
                    order.ShippingPhoneNumber,
                    order.ShippingCity,
                    order.ShippingAddressLine
                ),
                order.Total,
                items
            );
        }
    }
}