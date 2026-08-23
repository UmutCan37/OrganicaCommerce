using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Contracts.Orders
{
    public class UpdateOrderStatusRequest
    {
        public string NewStatus { get; set; } = string.Empty;
    }
}
