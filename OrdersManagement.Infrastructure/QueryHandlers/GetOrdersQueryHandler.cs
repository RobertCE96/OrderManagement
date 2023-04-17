using MediatR;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.QueryHandlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<PaginatedList<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrdersAsync(request.PageNumber, request.PageSize);
        }
    }

}
