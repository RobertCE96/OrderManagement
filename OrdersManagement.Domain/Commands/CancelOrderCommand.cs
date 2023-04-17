using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Commands
{
    public class CancelOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }

}
