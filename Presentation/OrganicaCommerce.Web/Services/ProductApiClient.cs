using OrganicaCommerce.Contracts.Products;
using System.Net.Http.Json;

namespace OrganicaCommerce.Web.Services
{
    public class ProductApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductListItemResponse>> GetListAsync(Guid? categoryId = null)
        {
            var url = "api/products";
            if (categoryId.HasValue)
                url += $"?categoryId={categoryId.Value}";

            var result = await _httpClient.GetFromJsonAsync<List<ProductListItemResponse>>(url);
            return result ?? new List<ProductListItemResponse>();
        }

        public async Task<ProductDetailResponse?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDetailResponse>();
        }

        public async Task<bool> CreateAsync(CreateProductRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStockAsync(Guid id, UpdateStockRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}/stock", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}