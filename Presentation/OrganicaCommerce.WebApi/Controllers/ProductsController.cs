using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Application.CQRS.Products.Commands;
using OrganicaCommerce.Application.CQRS.Products.Queries;
using OrganicaCommerce.Contracts.Products;

namespace OrganicaCommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] Guid? categoryId)
        {
            var result = await _mediator.Send(new GetProductListQuery { CategoryId = categoryId });
            var response = _mapper.Map<List<ProductListItemResponse>>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result is null)
                return NotFound();

            var response = _mapper.Map<ProductDetailResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var command = new CreateProductCommand
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId
            };

            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand
            {
                ProductId = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockRequest request)
        {
            var command = new OrganicaCommerce.Application.CQRS.Products.Commands.UpdateStockCommand
            {
                ProductId = id,
                Quantity = request.Quantity,
                IsIncrease = request.IsIncrease
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteProductCommand(id));

            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
