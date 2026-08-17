using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Admin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Admin.Facades
{
    public class DashboardFacade : IDashboardFacade
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetDashboardStatsResult> GetDashboardDataAsync()
        {
            var totalOrderCount = await _unitOfWork.Orders.GetTotalOrderCountAsync();
            var totalRevenue = await _unitOfWork.Orders.GetTotalRevenueAsync();
            var allProducts = await _unitOfWork.Products.GetAllAsync();
            var lowStockProducts = await _unitOfWork.Products.GetLowStockProductsAsync(threshold: 5);

            return new GetDashboardStatsResult
            {
                TotalOrderCount = totalOrderCount,
                TotalRevenue = totalRevenue,
                TotalProductCount = allProducts.Count,
                LowStockProducts = lowStockProducts.Select(p => new LowStockProductResult
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Stock = p.Stock
                }).ToList()
            };
        }
    }
}
