using MediatR;
using NexusPDV.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.GetAll
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return products?.Select(p => new GetAllProductsResponse
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            }) ?? Enumerable.Empty<GetAllProductsResponse>();
        }

    }
}
