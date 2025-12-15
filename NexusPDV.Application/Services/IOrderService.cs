using NexusPDV.Application.InputModels;
using NexusPDV.Application.ViewModels;
using System.Threading.Tasks;

namespace NexusPDV.Application.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> PlaceOrder(PlaceOrderInputModel input);
        Task<OrderViewModel> GetById(int id);
    }
}