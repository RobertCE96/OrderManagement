using MediatR;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Domain.Queries;
using OrdersManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.QueryHandlers
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                throw new NotFoundException($"Not found {nameof(Order)} with Id {request.OrderId}");
            }

            return new Order
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                DeliveryAddress = order.DeliveryAddress,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.OrderItems != null ? new Money(order.OrderItems.Sum(x => x.TotalPrice.Amount), "USD") : null,
                OrderItems = order.OrderItems != null ? order.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    ProductName = oi.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() : null
            };
        }
    }
}
