using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Contracts.Orders;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly OrderApiClient _orderApiClient;

        public AdminOrdersController(OrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderApiClient.GetListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var order = await _orderApiClient.GetByIdAsync(id);

            if (order is null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, string newStatus)
        {
            var request = new UpdateOrderStatusRequest { NewStatus = newStatus };
            await _orderApiClient.UpdateStatusAsync(id, request);
            return RedirectToAction(nameof(Detail), new { id });
        }
    }
}