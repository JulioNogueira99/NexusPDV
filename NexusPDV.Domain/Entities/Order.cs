using NexusPDV.Domain.Entities.Base;
using NexusPDV.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Entities
{
    public class Order : Entity
    {
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public decimal TotalAmount { get; private set; }

        protected Order() { }

        public Order(int customerId)
        {
            if (customerId <= 0) throw new ArgumentException("Cliente inválido");

            CustomerId = customerId;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Created;
            _items = new List<OrderItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero");

            var item = new OrderItem(product.Id, quantity, product.Price);
            _items.Add(item);

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.TotalItemPrice);
        }

        public void CompleteOrder()
        {
            Status = OrderStatus.Completed;
        }
    }
}
