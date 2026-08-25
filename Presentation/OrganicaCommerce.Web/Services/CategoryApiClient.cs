using OrganicaCommerce.Contracts.Categories;
using System.Net.Http.Json;

namespace OrganicaCommerce.Web.Services
{
    public class CategoryApiClient
    {
        private readonly HttpClient _httpClient;

        public CategoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryResponse>> GetListAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/categories");
            return result ?? new List<CategoryResponse>();
        }

        public async Task<bool> CreateAsync(CreateCategoryRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/categories", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}