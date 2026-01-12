using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Orders.PlaceOrder
{
    public class PlaceOrderCommand : IRequest<PlaceOrderResponse>
    {
        public int CustomerId { get; set; }
        public List<OrderItemInput> Items { get; set; } = new();
    }

    public class OrderItemInput
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
