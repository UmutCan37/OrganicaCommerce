using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Orders.Results
{
    public class CreateOrderResult
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
