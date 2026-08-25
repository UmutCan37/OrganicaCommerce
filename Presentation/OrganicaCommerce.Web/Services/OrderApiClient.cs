using OrganicaCommerce.Contracts.Orders;
using System.Net.Http.Json;

namespace OrganicaCommerce.Web.Services
{
    public class OrderApiClient
    {
        private readonly HttpClient _httpClient;

        public OrderApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderListItemResponse>> GetListAsync(Guid? userId = null)
        {
            var url = "api/orders";
            if (userId.HasValue)
                url += $"?userId={userId.Value}";

            var result = await _httpClient.GetFromJsonAsync<List<OrderListItemResponse>>(url);
            return result ?? new List<OrderListItemResponse>();
        }

        public async Task<OrderDetailResponse?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/orders/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<OrderDetailResponse>();
        }

        public async Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
        }

        public async Task<bool> UpdateStatusAsync(Guid id, UpdateOrderStatusRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}/status", request);
            return response.IsSuccessStatusCode;
        }
    }
}