using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Contracts.Orders;
using OrganicaCommerce.Web.Common;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderApiClient _orderApiClient;

        public OrdersController(OrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var request = new CreateOrderRequest
            {
                UserId = CurrentUserContext.UserId
            };

            var result = await _orderApiClient.CreateAsync(request);

            if (result is null)
                return BadRequest();

            return Ok(new { orderId = result.OrderId });
        }

        public async Task<IActionResult> Confirmation(Guid id)
        {
            var order = await _orderApiClient.GetByIdAsync(id);

            if (order is null)
                return NotFound();

            return View(order);
        }
    }
}