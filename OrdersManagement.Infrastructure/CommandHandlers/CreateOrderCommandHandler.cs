using MediatR;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                DeliveryAddress = request.DeliveryAddress,
                OrderItems = request.OrderItems,
                TotalAmount = request.OrderItems != null ? new Money(request.OrderItems.Sum(x => x.TotalPrice.Amount), "USD") : null,
            };

            var result = await _repository.CreateAsync(order);

            order.Id = result;

            return order;
        }
    }
}
