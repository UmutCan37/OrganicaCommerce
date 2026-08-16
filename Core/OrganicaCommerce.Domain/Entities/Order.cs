using OrganicaCommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrderItem> Items { get; set; } = new();

        public decimal GetTotal() => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}
