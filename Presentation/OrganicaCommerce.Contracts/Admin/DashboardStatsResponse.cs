using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Contracts.Admin
{
    public class DashboardStatsResponse
    {
        public int TotalOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalProductCount { get; set; }
        public List<LowStockProductResponse> LowStockProducts { get; set; } = new();
    }
}
