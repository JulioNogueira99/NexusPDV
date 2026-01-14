using NexusPDV.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int OrderId { get; private set; } // FK
        public int ProductId { get; private set; } // FK
        public Product Product { get; private set; }

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal TotalItemPrice => Quantity * UnitPrice;

        protected OrderItem() { }

        public OrderItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
