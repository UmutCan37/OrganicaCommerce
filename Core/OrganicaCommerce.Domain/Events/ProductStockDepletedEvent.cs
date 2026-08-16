using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Domain.Events
{
    public class ProductStockDepletedEvent
    {
        public Guid ProductId { get; }
        public int RemainingStock { get; }
        public DateTime OccurredAt { get; }
        public ProductStockDepletedEvent(Guid productId, int remainingStock)
        {
            ProductId = productId;
            RemainingStock = remainingStock;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
