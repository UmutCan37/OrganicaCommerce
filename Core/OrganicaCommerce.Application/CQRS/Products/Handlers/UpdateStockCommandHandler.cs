using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Products.Handlers
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

            if (product is null)
                return false;

            if (request.IsIncrease)
                product.IncreaseStock(request.Quantity);
            else
                product.DecreaseStock(request.Quantity);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
