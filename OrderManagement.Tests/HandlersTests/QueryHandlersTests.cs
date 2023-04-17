using Moq;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Domain.Queries;
using OrdersManagement.Infrastructure;
using OrdersManagement.Infrastructure.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.Tests.HandlersTests
{
    public class GetOrderQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly GetOrderQueryHandler _handler;

        public GetOrderQueryHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _handler = new GetOrderQueryHandler(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Order_When_Order_Exists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);

            var query = new GetOrderQuery { OrderId = orderId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Order_Does_Not_Exist()
        {
            // Arrange
            var query = new GetOrderQuery { OrderId = Guid.NewGuid() };
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(query.OrderId)).ReturnsAsync((Order)null);
            var handler = new GetOrderQueryHandler(_orderRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }

        public class GetOrdersQueryHandlerTests
        {
            private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
            private readonly GetOrdersQueryHandler _handler;

            public GetOrdersQueryHandlerTests()
            {
                _handler = new GetOrdersQueryHandler(_orderRepositoryMock.Object);
            }

            [Fact]
            public async Task GetOrdersQueryHandler_Handle_Should_Return_Paginated_List_Of_Orders()
            {
                // Arrange
                var query = new GetOrdersQuery { PageNumber = 1, PageSize = 10 };
                var orders = new List<Order>
                    {
                        new Order { Id = Guid.NewGuid() },
                        new Order { Id = Guid.NewGuid() },
                        new Order { Id = Guid.NewGuid() }
                    };
                var paginatedList = new PaginatedList<Order>(orders, orders.Count, query.PageNumber, query.PageSize);
                _orderRepositoryMock.Setup(repo => repo.GetOrdersAsync(query.PageNumber, query.PageSize)).ReturnsAsync(paginatedList);

                // Act
                var result = await _handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.Equal(paginatedList, result);
            }
        }
    }
}
