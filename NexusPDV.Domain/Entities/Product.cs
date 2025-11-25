using NexusPDV.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Entities
{
    public class Product : Entity
    {
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        protected Product() { }

        public Product(string title, decimal price, int stockQuantity)
        {
            ValidateDomain(title, price, stockQuantity);
            Title = title;
            Price = price;
            StockQuantity = stockQuantity;
        }

        private void ValidateDomain(string title, decimal price, int stock)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Título é obrigatório");
            if (price < 0) throw new ArgumentException("Preço não pode ser negativo");
            if (stock < 0) throw new ArgumentException("Estoque inicial não pode ser negativo");
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;

            if (StockQuantity < quantity)
            {
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {StockQuantity}, Solicitado: {quantity}");
            }
            StockQuantity -= quantity;
        }
    }
}
