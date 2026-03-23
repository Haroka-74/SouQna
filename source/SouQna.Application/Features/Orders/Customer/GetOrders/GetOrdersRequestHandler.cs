using MediatR;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.Customer.GetOrders
{
    public class GetOrdersRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetOrdersRequest, Page<DTOs.OrderDTO>>
    {
        public async Task<Page<DTOs.OrderDTO>> Handle(
            GetOrdersRequest request,
            CancellationToken cancellationToken
        )
        {
            Expression<Func<Order, bool>> filter = o =>
                (o.UserId == request.UserId) &&
                (!request.OrderStatus.HasValue || o.OrderStatus == request.OrderStatus);

            var (items, totalCount) = await unitOfWork.Orders.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                q => q.OrderByDescending(o => o.CreatedAt)
            );

            var orders = items.Select(
                i => new DTOs.OrderDTO(
                    i.Id,
                    i.OrderNumber,
                    i.Total,
                    i.OrderStatus,
                    i.CreatedAt
                )
            ).ToList();

            return new Page<DTOs.OrderDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                orders.AsReadOnly()
            );
        }
    }
}