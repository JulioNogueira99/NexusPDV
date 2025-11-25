using NexusPDV.Domain.Entities;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);

        void Update(Product product);
    }
}