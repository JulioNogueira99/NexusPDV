using NexusPDV.Application.InputModels;
using NexusPDV.Application.ViewModels;
using NexusPDV.Domain.Entities;
using NexusPDV.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace NexusPDV.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderViewModel> PlaceOrder(PlaceOrderInputModel input)
        {
            var order = new Order(input.CustomerId);

            foreach (var itemInput in input.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemInput.ProductId);

                if (product == null)
                {
                    throw new Exception($"Produto com ID {itemInput.ProductId} não encontrado.");
                }

                product.DebitStock(itemInput.Quantity);

                order.AddItem(product, itemInput.Quantity);

                _productRepository.Update(product);
            }

            _orderRepository.Add(order);

            await _unitOfWork.Commit();

            return new OrderViewModel
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.OrderDate,
                Status = order.Status.ToString()
            };
        }
    }
}