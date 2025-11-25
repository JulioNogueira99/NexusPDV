using Moq;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.Services;
using NexusPDV.Domain.Entities;
using NexusPDV.Domain.Interfaces;
using Xunit;

namespace NexusPDV.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task PlaceOrder_WithValidData_ShouldGenerateOrderIdAndReduceStock()
        {
            var customerRepoMock = new Mock<IOrderRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var fakeProduct = new Product("Test Product", 100.0m, 10);

            var propId = fakeProduct.GetType().GetProperty("Id");
            propId.SetValue(fakeProduct, 1);

            productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeProduct);

            unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(true);

            var orderService = new OrderService(
                customerRepoMock.Object,
                productRepoMock.Object,
                unitOfWorkMock.Object
            );

            var input = new PlaceOrderInputModel
            {
                CustomerId = 1,
                Items = new List<OrderItemInputModel>
                {
                    new OrderItemInputModel { ProductId = 1, Quantity = 2 }
                }
            };

            var result = await orderService.PlaceOrder(input);

            Assert.NotNull(result);

            Assert.Equal(8, fakeProduct.StockQuantity);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async Task PlaceOrder_WithInsufficientStock_ShouldThrowExceptionAndNotSave()
        {
            var customerRepoMock = new Mock<IOrderRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var fakeProduct = new Product("Low Stock Product", 100.0m, 5);
            var propId = fakeProduct.GetType().GetProperty("Id");
            propId.SetValue(fakeProduct, 2);

            productRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(fakeProduct);

            var orderService = new OrderService(
                customerRepoMock.Object,
                productRepoMock.Object,
                unitOfWorkMock.Object
            );

            var input = new PlaceOrderInputModel
            {
                CustomerId = 1,
                Items = new List<OrderItemInputModel>
                {
                    new OrderItemInputModel { ProductId = 2, Quantity = 10 }
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => orderService.PlaceOrder(input));
            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }
    }
}