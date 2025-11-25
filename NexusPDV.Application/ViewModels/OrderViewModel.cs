using System;

namespace NexusPDV.Application.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}