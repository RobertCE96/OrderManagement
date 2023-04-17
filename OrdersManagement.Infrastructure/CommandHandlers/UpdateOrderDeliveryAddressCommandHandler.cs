using MediatR;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.CommandHandlers
{
    public class UpdateOrderDeliveryAddressCommandHandler : IRequestHandler<UpdateOrderDeliveryAddressCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderDeliveryAddressCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException($"Not found {nameof(Order)} with Id {request.OrderId}");
            }

            order.UpdateDeliveryAddress(request.DeliveryAddress);

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
