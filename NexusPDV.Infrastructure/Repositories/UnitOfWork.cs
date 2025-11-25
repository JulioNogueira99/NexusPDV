using NexusPDV.Domain.Interfaces;
using NexusPDV.Infrastructure.Context;
using System.Threading.Tasks;

namespace NexusPDV.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}