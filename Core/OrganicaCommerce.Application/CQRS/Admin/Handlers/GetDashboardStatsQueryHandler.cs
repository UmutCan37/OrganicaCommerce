using MediatR;
using OrganicaCommerce.Application.Common.Interfaces;
using OrganicaCommerce.Application.CQRS.Admin.Queries;
using OrganicaCommerce.Application.CQRS.Admin.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.CQRS.Admin.Handlers
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, GetDashboardStatsResult>
    {
        private readonly IDashboardFacade _dashboardFacade;

        public GetDashboardStatsQueryHandler(IDashboardFacade dashboardFacade)
        {
            _dashboardFacade = dashboardFacade;
        }

        public async Task<GetDashboardStatsResult> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardFacade.GetDashboardDataAsync();
        }
    }
}
