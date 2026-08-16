using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Products.Queries;
using OrganicaCommerce.Application.CQRS.Products.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Products.Handlers
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<GetProductListResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetProductListResult>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = request.CategoryId.HasValue
                ? await _unitOfWork.Products.GetByCategoryAsync(request.CategoryId.Value)
                : await _unitOfWork.Products.GetAllAsync();

            return products.Select(p => new GetProductListResult
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            }).ToList();
        }
    }
}
