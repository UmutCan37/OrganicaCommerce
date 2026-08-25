using OrganicaCommerce.Contracts.Admin;
using System.Net.Http.Json;

namespace OrganicaCommerce.Web.Services
{
    public class AdminApiClient
    {
        private readonly HttpClient _httpClient;

        public AdminApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardStatsResponse?> GetDashboardStatsAsync()
        {
            return await _httpClient.GetFromJsonAsync<DashboardStatsResponse>("api/admin/dashboard");
        }
    }
}