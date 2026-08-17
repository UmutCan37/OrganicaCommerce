using MediatR;
using Microsoft.Extensions.Logging;
using OrganicaCommerce.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Yeni sipariş oluşturuldu. OrderId: {OrderId}, UserId: {UserId}, Tarih: {OccurredAt}",
                notification.OrderId, notification.UserId, notification.OccurredAt);

            return Task.CompletedTask;
        }
    }
}
