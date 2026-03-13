using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.GetAll
{
    public class GetAllProductsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
