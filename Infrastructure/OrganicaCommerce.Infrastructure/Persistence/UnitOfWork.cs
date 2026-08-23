using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Infrastructure.Persistence.Repositories;

namespace OrganicaCommerce.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProductRepository? _products;
        private ICategoryRepository? _categories;
        private ICartRepository? _carts;
        private IOrderRepository? _orders;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _products ??= new ProductRepository(_context);
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
        public ICartRepository Carts => _carts ??= new CartRepository(_context);
        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}