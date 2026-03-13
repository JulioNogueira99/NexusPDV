using NexusPDV.Domain.Entities;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();

        void Update(Product product);
    }
}