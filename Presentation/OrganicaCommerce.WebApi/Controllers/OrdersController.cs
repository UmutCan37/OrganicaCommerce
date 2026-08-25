using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganicaCommerce.Application.CQRS.Orders.Commands;
using OrganicaCommerce.Application.CQRS.Orders.Queries;
using OrganicaCommerce.Contracts.Orders;
using OrganicaCommerce.Domain.Enums;

namespace OrganicaCommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] Guid? userId)
        {
            var result = await _mediator.Send(new GetOrderListQuery { UserId = userId });
            var response = _mapper.Map<List<OrderListItemResponse>>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));

            if (result is null)
                return NotFound();

            var response = _mapper.Map<OrderDetailResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var command = new CreateOrderCommand
            {
                UserId = request.UserId
            };

            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateOrderResponse>(result);
            return CreatedAtAction(nameof(GetById), new { id = response.OrderId }, response);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusRequest request)
        {
            if (!Enum.TryParse<OrderStatus>(request.NewStatus, ignoreCase: true, out var newStatus))
                return BadRequest("Geçersiz sipariş durumu.");

            var command = new UpdateOrderStatusCommand
            {
                OrderId = id,
                NewStatus = newStatus
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
