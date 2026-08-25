using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Contracts.Orders
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
