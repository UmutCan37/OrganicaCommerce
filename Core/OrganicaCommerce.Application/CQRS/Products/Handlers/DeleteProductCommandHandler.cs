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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

            if (product is null)
                return false;

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
