using System;
using System.Collections.Generic; // Adicione

namespace NexusPDV.Application.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public List<OrderItemViewModel> Items { get; set; } // Nova Propriedade
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}