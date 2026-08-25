using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminApiClient _adminApiClient;

        public AdminController(AdminApiClient adminApiClient)
        {
            _adminApiClient = adminApiClient;
        }

        public async Task<IActionResult> Dashboard()
        {
            var stats = await _adminApiClient.GetDashboardStatsAsync();
            return View(stats);
        }
    }
}