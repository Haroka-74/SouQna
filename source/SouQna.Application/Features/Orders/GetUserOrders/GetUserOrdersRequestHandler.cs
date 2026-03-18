using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.Features.Orders.Shared;

namespace SouQna.Application.Features.Orders.GetUserOrders
{
    public class GetUserOrdersRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetUserOrdersRequest, Page<OrderDTO>>
    {
        public async Task<Page<OrderDTO>> Handle(
            GetUserOrdersRequest request,
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

            var orders = mapper.Map<List<OrderDTO>>(items);

            return new Page<OrderDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                orders.AsReadOnly()
            );
        }
    }
}