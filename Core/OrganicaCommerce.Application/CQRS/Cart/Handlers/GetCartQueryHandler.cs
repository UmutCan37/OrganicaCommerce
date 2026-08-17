using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Cart.Queries;
using OrganicaCommerce.Application.CQRS.Cart.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Cart.Handlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, GetCartResult?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCartResult?> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetByUserIdAsync(request.UserId);
            if(cart is null)
            {
                return null;

            }
            return new GetCartResult
            {
                CartId = cart.Id,
                Total = cart.GetTotal(),
                Items = cart.Items.Select(i => new GetCartItemResult
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? string.Empty,
                    ImageUrl = i.Product?.ImageUrl,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };

        }
    }
}
