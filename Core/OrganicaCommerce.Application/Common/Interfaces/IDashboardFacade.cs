using OrganicaCommerce.Application.CQRS.Admin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.Common.Interfaces
{
    public interface IDashboardFacade
    {
        Task<GetDashboardStatsResult> GetDashboardDataAsync();
    }
}
