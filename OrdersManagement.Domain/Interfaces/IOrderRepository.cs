using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Guid> CreateAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task<PaginatedList<Order>> GetOrdersAsync(int pageNumber, int pageSize);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<int> GetTotalOrdersAsync();
    }
}
