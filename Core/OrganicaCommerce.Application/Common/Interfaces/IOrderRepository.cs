using OrganicaCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.Common.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetByUserIdAsync(Guid userId);
        Task<int> GetTotalOrderCountAsync();
        Task<decimal> GetTotalRevenueAsync();

        Task<Order?> GetByIdWithItemsAsync(Guid id);
    }
}
