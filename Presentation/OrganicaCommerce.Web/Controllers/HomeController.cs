using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductApiClient _productApiClient;
        private readonly CategoryApiClient _categoryApiClient;

        public HomeController(ProductApiClient productApiClient, CategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetListAsync();
            var categories = await _categoryApiClient.GetListAsync();
            ViewBag.Categories = categories;
            return View(products);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}