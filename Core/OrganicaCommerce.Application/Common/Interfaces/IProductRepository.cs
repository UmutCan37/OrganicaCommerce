using OrganicaCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.Common.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetByCategoryAsync(Guid categoryId);
        Task<List<Product>> GetLowStockProductsAsync(int threshold);
    }
}
