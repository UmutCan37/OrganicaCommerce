using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Admin.Results
{
    public class GetDashboardStatsResult
    {
        public int TotalOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalProductCount { get; set; }
        public List<LowStockProductResult> LowStockProducts { get; set; } = new();
    }

    public class LowStockProductResult
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
