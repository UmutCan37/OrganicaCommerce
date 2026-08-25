using OrganicaCommerce.Contracts.Cart;
using System.Net.Http.Json;

namespace OrganicaCommerce.Web.Services
{
    public class CartApiClient
    {
        private readonly HttpClient _httpClient;

        public CartApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartResponse?> GetCartAsync(Guid userId)
        {
            return await _httpClient.GetFromJsonAsync<CartResponse>($"api/cart/{userId}");
        }

        public async Task<bool> AddToCartAsync(AddToCartRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cart", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveFromCartAsync(Guid userId, Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"api/cart/{userId}/items/{productId}");
            return response.IsSuccessStatusCode;
        }
    }
}