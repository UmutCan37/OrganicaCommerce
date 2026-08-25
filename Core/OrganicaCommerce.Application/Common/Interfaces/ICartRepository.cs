using OrganicaCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.Common.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task AddCartItemAsync(CartItem cartItem);
        Task<Cart?> GetByUserIdAsync(Guid userId);
    }
}
