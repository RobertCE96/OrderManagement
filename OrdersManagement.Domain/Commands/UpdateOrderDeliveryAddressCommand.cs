using MediatR;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Commands
{
    public class UpdateOrderDeliveryAddressCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}
