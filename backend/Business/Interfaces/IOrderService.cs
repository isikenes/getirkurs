using Business.DTOs;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(int courseId, string userId);
        Task<OrderDTO> GetOrder(int id);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserId(string userId);
    }
}
