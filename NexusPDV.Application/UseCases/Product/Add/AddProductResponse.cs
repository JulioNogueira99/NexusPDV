using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.Add
{
    public class AddProductResponse
    {
        public int ProductId { get; internal set; }
        public string Title { get; internal set; }
        public decimal Price { get; internal set; }
        public int StockQuantity { get; internal set; }
    }
}
