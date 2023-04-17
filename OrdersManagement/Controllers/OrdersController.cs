using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Queries;
using OrdersManagement.Infrastructure;

namespace OrdersManagement.Controllers
{
    [ApiController]
    //[Authorize] get token not done
    [Route("api")]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("orders")]
        public async Task<ActionResult<Guid>> CreateOrder(CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order.Id);
        }

        [HttpPut("orders/{orderId}/delivery-address")]
        public async Task<IActionResult> UpdateOrderDeliveryAddress(Guid orderId, UpdateOrderDeliveryAddressCommand command)
        {
            if (orderId != command.OrderId)
            {
                return BadRequest("The provided OrderId does not match the one in the request body.");
            }

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("orders/{orderId}/items")]
        public async Task<IActionResult> UpdateOrderItems(Guid orderId, UpdateOrderItemsCommand command)
        {
            if (orderId != command.OrderId)
            {
                return BadRequest("The provided OrderId does not match the one in the request body.");
            }

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("orders/{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            await _mediator.Send(new CancelOrderCommand { OrderId = orderId });
            return Ok();
        }

        [HttpGet("orders/{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(Guid orderId)
        {
            var order = await _mediator.Send(new GetOrderQuery { OrderId = orderId });
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("orders")]
        public async Task<ActionResult<PaginatedList<Order>>> GetOrders([FromQuery] GetOrdersQuery query)
        {
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
    };
}
