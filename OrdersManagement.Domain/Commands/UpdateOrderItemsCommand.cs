using MediatR;
using OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Commands
{
    public class UpdateOrderItemsCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
