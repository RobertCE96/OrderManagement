using MediatR;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.Domain.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Address DeliveryAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
