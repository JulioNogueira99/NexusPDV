using Moq;
using NexusPDV.Application.UseCases.Orders.PlaceOrder;
using NexusPDV.Domain.Entities;
using NexusPDV.Domain.Interfaces;
using Xunit;

namespace NexusPDV.Tests
{
    public class PlaceOrderHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public PlaceOrderHandlerTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _productRepoMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldGenerateOrderIdAndReduceStock()
        {
            // 1. Arrange (Preparação)

            // Criando o produto fake
            var fakeProduct = new Product("Test Product", 100.0m, 10);

            // Reflection para setar o ID
            var propId = fakeProduct.GetType().GetProperty("Id");
            propId?.SetValue(fakeProduct, 1);

            // Configurando o comportamento do Mock
            _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeProduct);
            _unitOfWorkMock.Setup(u => u.Commit()).ReturnsAsync(true);

            var handler = new PlaceOrderHandler(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new PlaceOrderCommand
            {
                CustomerId = 1,
                Items = new List<OrderItemInput>
            {
                new OrderItemInput { ProductId = 1, Quantity = 2 }
            }
            };

            // 2. Act (Execução)
            // O método do MediatR sempre pede um CancellationToken
            var result = await handler.Handle(command, CancellationToken.None);

            // 3. Assert (Verificação)
            Assert.NotNull(result);

            // Verifica se a lógica de domínio (baixar estoque) funcionou
            Assert.Equal(8, fakeProduct.StockQuantity);

            // Verifica se o Commit foi chamado
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithInsufficientStock_ShouldThrowExceptionAndNotSave()
        {
            // 1. Arrange
            var fakeProduct = new Product("Low Stock Product", 100.0m, 5);
            var propId = fakeProduct.GetType().GetProperty("Id");
            propId?.SetValue(fakeProduct, 2);

            _productRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(fakeProduct);

            var handler = new PlaceOrderHandler(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _unitOfWorkMock.Object
            );

            var command = new PlaceOrderCommand
            {
                CustomerId = 1,
                Items = new List<OrderItemInput>
            {
                new OrderItemInput { ProductId = 2, Quantity = 10 } // Pedindo mais que o estoque (10 > 5)
            }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }
    }
}