using MediatR;
using OrganicaCommerce.Application.CQRS.Orders.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Orders.Queries
{
    public class GetOrderListQuery : IRequest<List<GetOrderListResult>>
    {
        public Guid? UserId { get; set; }
    }
}
