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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.Include(c => c.Items)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
