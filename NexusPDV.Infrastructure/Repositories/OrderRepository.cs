using NexusPDV.Domain.Entities;
using NexusPDV.Domain.Interfaces;
using NexusPDV.Infrastructure.Context;

namespace NexusPDV.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }
    }
}