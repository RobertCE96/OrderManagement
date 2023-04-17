using MediatR;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.CommandHandlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order == null)
            {
                throw new NotFoundException($" Order with id {command.OrderId} not found");
            }

            order.Cancel();

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
