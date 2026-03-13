using Microsoft.EntityFrameworkCore;
using NexusPDV.Domain.Entities;
using NexusPDV.Domain.Interfaces;
using NexusPDV.Infrastructure.Context;
using System.Threading.Tasks;

namespace NexusPDV.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}