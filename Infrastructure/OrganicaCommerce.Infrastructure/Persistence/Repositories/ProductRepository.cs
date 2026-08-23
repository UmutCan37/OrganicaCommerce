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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet.Include(x=>x.Category).Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public Task<List<Product>> GetLowStockProductsAsync(int threshold)
        {
            return _dbSet.Where(p => p.Stock < threshold).ToListAsync();
        }
    }
}
