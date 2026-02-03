using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.Add
{
    public class AddProductCommand : IRequest<AddProductResponse>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
