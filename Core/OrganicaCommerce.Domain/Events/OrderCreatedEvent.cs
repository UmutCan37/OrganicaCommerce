using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; }
        public Guid UserId { get; }
        public DateTime OccurredAt { get; }

        public OrderCreatedEvent(Guid orderId, Guid userId)
        {
            OrderId = orderId;
            UserId = userId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
