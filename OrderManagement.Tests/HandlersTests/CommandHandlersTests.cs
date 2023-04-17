using Moq;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Domain.ValueObjects;
using OrdersManagement.Infrastructure.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.Tests.HandlersTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_OrderId()
        {
            // Arrange
            var command = new CreateOrderCommand();
            var orderId = Guid.NewGuid();
            _orderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Order>())).ReturnsAsync(orderId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task Handle_Should_Add_Order_To_Repository()
        {
            // Arrange
            var command = new CreateOrderCommand();
            var order = new Order { Id = Guid.NewGuid() };

            _orderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Order>())).ReturnsAsync(order.Id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(order.Id, result.Id);
        }
    }

    public class UpdateOrderDeliveryAddressCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly UpdateOrderDeliveryAddressCommandHandler _handler;

        public UpdateOrderDeliveryAddressCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _handler = new UpdateOrderDeliveryAddressCommandHandler(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Update_Order_DeliveryAddress()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderDeliveryAddressCommand { OrderId = orderId, DeliveryAddress = new Address { City = "1", Country = "1", Street = "1" } };
            var order = new Order { Id = orderId };
            _orderRepositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.DeliveryAddress, order.DeliveryAddress);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_If_Order_Not_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderDeliveryAddressCommand { OrderId = orderId, DeliveryAddress = new Address { City = "1", Country = "1", Street = "1" } };
            _orderRepositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
