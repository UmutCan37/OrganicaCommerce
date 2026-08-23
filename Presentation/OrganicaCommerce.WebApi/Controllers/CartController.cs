using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Application.CQRS.Cart.Commands;
using OrganicaCommerce.Application.CQRS.Cart.Queries;
using OrganicaCommerce.Contracts.Cart;

namespace OrganicaCommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var result = await _mediator.Send(new GetCartQuery(userId));

            if (result is null)
                return Ok(new CartResponse { CartId = Guid.Empty, Items = new(), Total = 0 });

            var response = _mapper.Map<CartResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var command = new AddToCartCommand
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound("Ürün bulunamadı.");

            return NoContent();
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveFromCart(Guid userId, Guid productId)
        {
            var command = new RemoveFromCartCommand
            {
                UserId = userId,
                ProductId = productId
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
