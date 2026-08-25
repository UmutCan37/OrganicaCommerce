using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Contracts.Products;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ProductApiClient _productApiClient;
        private readonly CategoryApiClient _categoryApiClient;

        public AdminProductsController(ProductApiClient productApiClient, CategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetListAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryApiClient.GetListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var result = await _productApiClient.CreateAsync(request);

            if (!result)
            {
                ViewBag.Categories = await _categoryApiClient.GetListAsync();
                ModelState.AddModelError("", "Ürün oluşturulurken bir hata oluştu.");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productApiClient.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            ViewBag.Categories = await _categoryApiClient.GetListAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateProductRequest request)
        {
            var success = await _productApiClient.UpdateAsync(id, request);

            if (!success)
            {
                ViewBag.Categories = await _categoryApiClient.GetListAsync();
                ModelState.AddModelError("", "Ürün güncellenirken bir hata oluştu.");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity, bool isIncrease)
        {
            var request = new UpdateStockRequest { Quantity = quantity, IsIncrease = isIncrease };
            await _productApiClient.UpdateStockAsync(id, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productApiClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}