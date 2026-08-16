using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public Guid ProductId { get; }
        public int RequestedQuantity { get; }
        public int AvailableStock { get; }

        public InsufficientStockException(Guid productId, int requestedQuantity, int availableStock)
            : base($"Ürün stoğu yetersiz. İstenen: {requestedQuantity}, Mevcut: {availableStock}")
        {
            ProductId = productId;
            RequestedQuantity = requestedQuantity;
            AvailableStock = availableStock;
        }
    }
}
