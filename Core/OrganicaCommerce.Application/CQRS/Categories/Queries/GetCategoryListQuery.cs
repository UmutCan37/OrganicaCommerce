using MediatR;
using OrganicaCommerce.Application.CQRS.Categories.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Categories.Queries
{
    public class GetCategoryListQuery : IRequest<List<GetCategoryListResult>>
    {
    }
}
