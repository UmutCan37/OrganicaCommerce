using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductApiClient _productApiClient;
        private readonly CategoryApiClient _categoryApiClient;

        public ShopController(ProductApiClient productApiClient, CategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> List(Guid? categoryId)
        {
            var products = await _productApiClient.GetListAsync(categoryId);
            var categories = await _categoryApiClient.GetListAsync();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;

            return View(products);
        }
    }
}