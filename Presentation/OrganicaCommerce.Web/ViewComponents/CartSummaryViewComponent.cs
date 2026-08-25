using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Web.Common;
using OrganicaCommerce.Web.Services;

namespace OrganicaCommerce.Web.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly CartApiClient _cartApiClient;

        public CartSummaryViewComponent(CartApiClient cartApiClient)
        {
            _cartApiClient = cartApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _cartApiClient.GetCartAsync(CurrentUserContext.UserId);
            var itemCount = cart?.Items.Sum(i => i.Quantity) ?? 0;

            return View(itemCount);
        }
    }
}