using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Cart.Commands
{
    public class RemoveFromCartCommand :IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
