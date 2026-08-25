using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiClient _productApiClient;

        public ProductController(ProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await _productApiClient.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            return View(product);
        }
    }
}