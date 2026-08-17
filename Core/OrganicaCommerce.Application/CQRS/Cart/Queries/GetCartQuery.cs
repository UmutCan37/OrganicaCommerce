using MediatR;
using OrganicaCommerce.Application.CQRS.Cart.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Cart.Queries
{
    public class GetCartQuery:IRequest<GetCartResult?>
    {
        public Guid UserId { get; set; }

        public GetCartQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
