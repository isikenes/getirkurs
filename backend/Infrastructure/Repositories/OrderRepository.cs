using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public async Task AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
        }

        public async Task<Order> GetOrder(int id)
        {
            return await context.Orders.Include(o => o.Course).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await context.Orders.Include(o => o.Course).Where(o => o.AppUserId == userId).ToListAsync();
        }
    }
}
