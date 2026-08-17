using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Orders.Queries;
using OrganicaCommerce.Application.CQRS.Orders.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Orders.Handlers
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<GetOrderListResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetOrderListResult>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = request.UserId.HasValue
                ? await _unitOfWork.Orders.GetByUserIdAsync(request.UserId.Value)
                : await _unitOfWork.Orders.GetAllAsync();

            return orders.Select(o => new GetOrderListResult
            {
                OrderId = o.Id,
                UserId = o.UserId,
                Status = o.Status,
                CreatedDate = o.CreatedDate,
                Total = o.GetTotal(),
                ItemCount = o.Items.Count
            }).ToList();
        }
    }
}
