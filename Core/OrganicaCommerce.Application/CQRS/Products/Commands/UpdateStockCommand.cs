using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Products.Commands
{
    public class UpdateStockCommand: IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsIncrease { get; set; }
    }
}
