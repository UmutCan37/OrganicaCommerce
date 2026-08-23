using Microsoft.EntityFrameworkCore;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetByIdWithItemsAsync(Guid id)
        {
            return await _dbSet
    .Include(o => o.Items)
        .ThenInclude(i => i.Product)
    .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.Include(o=>o.Items).ThenInclude(i => i.Product).Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<int> GetTotalOrderCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var orders =await _dbSet.Include(o=>o.Items).ToListAsync();
            return orders.Sum(o => o.GetTotal());
        }
    }
}
