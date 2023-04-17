using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<PaginatedList<Order>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Orders.AsQueryable();

            int count = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Order>(items, count, pageNumber, pageSize);
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> GetTotalOrdersAsync()
        {
            return await _dbContext.Orders.CountAsync();
        }
    }
}
