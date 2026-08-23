using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Application.CQRS.Categories.Commands;
using OrganicaCommerce.Application.CQRS.Categories.Queries;
using OrganicaCommerce.Contracts.Categories;

namespace OrganicaCommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetCategoryListQuery());
            var response = _mapper.Map<List<CategoryResponse>>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var command = new CreateCategoryCommand
            {
                Name = request.Name,
                Description = request.Description
            };

            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetList), new { }, categoryId);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
        {
            var command = new UpdateCategoryCommand
            {
                CategoryId = id,
                Name = request.Name,
                Description = request.Description
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteCategoryCommand(id));

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
