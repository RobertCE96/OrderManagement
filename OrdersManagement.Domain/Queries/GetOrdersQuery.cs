using MediatR;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Queries
{
    public class GetOrdersQuery : IRequest<PaginatedList<Order>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
