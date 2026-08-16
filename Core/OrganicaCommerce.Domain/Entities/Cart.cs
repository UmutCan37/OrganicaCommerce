using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem is not null)
            {
                existingItem.Quantity += quantity;
                return;
            }

            Items.Add(new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            });
        }

        public void RemoveItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is not null)
                Items.Remove(item);
        }

        public decimal GetTotal() => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}

