using MediatR;
using NexusPDV.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.GetById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return null;
            }

            return new GetProductByIdResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }
    }
}
