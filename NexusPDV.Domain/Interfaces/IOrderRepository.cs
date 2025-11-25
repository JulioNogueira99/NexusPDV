using NexusPDV.Domain.Entities;

namespace NexusPDV.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
    }
}