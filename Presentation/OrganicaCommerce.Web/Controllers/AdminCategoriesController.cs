using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Contracts.Categories;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly CategoryApiClient _categoryApiClient;

        public AdminCategoriesController(CategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiClient.GetListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var success = await _categoryApiClient.CreateAsync(request);

            if (!success)
            {
                ModelState.AddModelError("", "Kategori oluşturulurken bir hata oluştu.");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categories = await _categoryApiClient.GetListAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateCategoryRequest request)
        {
            var success = await _categoryApiClient.UpdateAsync(id, request);

            if (!success)
            {
                ModelState.AddModelError("", "Kategori güncellenirken bir hata oluştu.");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryApiClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}