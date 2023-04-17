using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrdersManagement.Controllers;
using OrdersManagement.Domain.Commands;
using OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.Tests.ControllersTests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly OrdersController _ordersController;

        public OrdersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _ordersController = new OrdersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateOrder_Should_Return_Ok_With_OrderId()
        {
            // Arrange
            var command = new CreateOrderCommand();
            var orderId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(new Order { Id = orderId }); ; ;

            // Act
            var result = await _ordersController.CreateOrder(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultOrderId = Assert.IsType<Guid>(okResult.Value);
            Assert.Equal(orderId, resultOrderId);
        }

        [Fact]
        public async Task UpdateOrderDeliveryAddress_Should_Return_Ok()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderDeliveryAddressCommand { OrderId = orderId };
            _mediatorMock.Setup(m => m.Send(command, default)).Returns(Unit.Task);

            // Act
            var result = await _ordersController.UpdateOrderDeliveryAddress(orderId, command);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateOrderDeliveryAddress_Should_Return_BadRequest_If_OrderId_Does_Not_Match()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderDeliveryAddressCommand { OrderId = Guid.NewGuid() };

            // Act
            var result = await _ordersController.UpdateOrderDeliveryAddress(orderId, command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The provided OrderId does not match the one in the request body.", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public async Task UpdateOrderItems_Should_Return_Ok()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderItemsCommand { OrderId = orderId };
            _mediatorMock.Setup(m => m.Send(command, default)).Returns(Unit.Task);

            // Act
            var result = await _ordersController.UpdateOrderItems(orderId, command);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateOrderItems_Should_Return_BadRequest_If_OrderId_Does_Not_Match()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new UpdateOrderItemsCommand { OrderId = Guid.NewGuid() };

            // Act
            var result = await _ordersController.UpdateOrderItems(orderId, command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The provided OrderId does not match the one in the request body.", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public async Task CancelOrder_Should_Return_Ok()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelOrderCommand>(), default)).Returns(Unit.Task);

            // Act
            var result = await _ordersController.CancelOrder(orderId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}