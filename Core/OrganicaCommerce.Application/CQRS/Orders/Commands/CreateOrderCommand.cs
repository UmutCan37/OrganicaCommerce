using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Orders.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderCommand>
    {
        public Guid UserId { get; set; }
    }
}
