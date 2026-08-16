using MediatR;
using OrganicaCommerce.Application.CQRS.Products.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Products.Queries
{
    public class GetProductListQuery : IRequest<List<GetProductListResult>>
    {
        public Guid? CategoryId { get; set; }
    }
}
