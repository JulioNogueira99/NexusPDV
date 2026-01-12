using MediatR;
using NexusPDV.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Orders.GetById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                return null;
            }

            return new GetOrderByIdResponse
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.OrderDate,
                Status = order.Status.ToString(),

                Items = order.Items.Select(i => new OrderItemResponse
                {
                    ProductName = i.Product != null ? i.Product.Title : "Produto não carregado",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.TotalItemPrice
                }).ToList()
            };
        }
    }
}
