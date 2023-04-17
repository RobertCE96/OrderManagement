using MediatR;
using OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Queries
{
    public class GetOrderQuery : IRequest<Order>
    {
        public Guid OrderId { get; set; }
    }
}
