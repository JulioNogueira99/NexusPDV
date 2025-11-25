using System.Collections.Generic;

namespace NexusPDV.Application.InputModels
{
    public class PlaceOrderInputModel
    {
        public int CustomerId { get; set; }
        public List<OrderItemInputModel> Items { get; set; }
    }

    public class OrderItemInputModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}