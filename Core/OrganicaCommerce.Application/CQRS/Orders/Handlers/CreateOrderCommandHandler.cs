using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Orders.Commands;
using OrganicaCommerce.Application.CQRS.Orders.Results;
using OrganicaCommerce.Domain.Entities;
using OrganicaCommerce.Domain.Enums;
using OrganicaCommerce.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetByUserIdAsync(request.UserId);

            if (cart is null || !cart.Items.Any())
                throw new InvalidOperationException("Sepet boş, sipariş oluşturulamaz.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.UtcNow
            };

            foreach(var cartItem in cart.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(cartItem.ProductId);

                if (product is null)
                    continue;

                product.DecreaseStock(cartItem.Quantity);
                _unitOfWork.Products.Update(product);
                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                });

                if (product.Stock == 0)
                    await _mediator.Publish(new ProductStockDepletedEvent(product.Id, product.Stock), cancellationToken);

            }
            await _unitOfWork.Orders.AddAsync(order);

            _unitOfWork.Carts.Delete(cart);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Publish(new OrderCreatedEvent(order.Id, order.UserId), cancellationToken);

            return new CreateOrderResult
            {
                OrderId = order.Id,
                Total = order.GetTotal()
            };
        }
    }
}
