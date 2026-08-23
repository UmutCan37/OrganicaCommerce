using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Domain.Entities;

namespace OrganicaCommerce.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}