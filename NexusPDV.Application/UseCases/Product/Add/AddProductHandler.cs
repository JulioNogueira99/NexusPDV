using MediatR;
using NexusPDV.Domain.Interfaces;
using NexusPDV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.Add
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, AddProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product(request.Title, request.Price, request.StockQuantity);
            await _productRepository.AddAsync(product);
            await _unitOfWork.Commit();
            return new AddProductResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }
    }
}
