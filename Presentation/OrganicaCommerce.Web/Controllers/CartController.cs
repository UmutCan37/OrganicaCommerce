using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Contracts.Cart;
using OrganicaCommerce.Web.Common;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly CartApiClient _cartApiClient;

        public CartController(CartApiClient cartApiClient)
        {
            _cartApiClient = cartApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartApiClient.GetCartAsync(CurrentUserContext.UserId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestModel model)
        {
            var request = new AddToCartRequest
            {
                UserId = CurrentUserContext.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            var success = await _cartApiClient.AddToCartAsync(request);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            var success = await _cartApiClient.RemoveFromCartAsync(CurrentUserContext.UserId, productId);

            if (!success)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
    }
}

public class AddToCartRequestModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}