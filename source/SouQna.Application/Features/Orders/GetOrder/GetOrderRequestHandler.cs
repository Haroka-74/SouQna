using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetOrder
{
    public class GetOrderRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetOrderRequest, OrderDetailDTO>
    {
        public async Task<OrderDetailDTO> Handle(
            GetOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.OrderId && o.UserId == request.UserId,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {request.OrderId}) was not found");

            var items = mapper.Map<List<OrderItemDTO>>(order.OrderItems);

            return new OrderDetailDTO(
                order.OrderNumber,
                new ShippingInfoDTO(
                    order.ShippingFullName,
                    order.ShippingPhoneNumber,
                    order.ShippingCity,
                    order.ShippingAddressLine
                ),
                order.Total,
                order.OrderStatus,
                order.CreatedAt,
                order.ConfirmedAt,
                order.ShippedAt,
                order.DeliveredAt,
                order.CancelledAt,
                items
            );
        }
    }
}