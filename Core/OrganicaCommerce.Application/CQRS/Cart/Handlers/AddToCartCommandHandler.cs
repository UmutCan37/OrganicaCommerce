using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Cart.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Cart.Handlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

            if(product is null)
            {
                return false;
            }
            var cart = await _unitOfWork.Carts.GetByUserIdAsync(request.UserId);

            if(cart is null)
            {
                cart = new Domain.Entities.Cart
                {
                    Id = Guid.NewGuid(),
                    UserId=request.UserId,
                    CreatedDate=DateTime.UtcNow
                };
                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();

            }
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (existingItem is not null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var newItem = new Domain.Entities.CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price
                };

                await _unitOfWork.Carts.AddCartItemAsync(newItem);
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
