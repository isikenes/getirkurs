using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    }
}
