using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;

namespace Business.Services
{
    public class OrderService(IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task CreateOrder(int courseId, string userId)
        {
            var course = await unitOfWork.Courses.GetById(courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var order = new Order
            {
                CourseId = courseId,
                AppUserId = userId,
                OrderDate = DateTime.Now,
                Course = course
            };

            await unitOfWork.Orders.AddOrder(order);
            await unitOfWork.Save();
        }

        public async Task<OrderDTO> GetOrder(int id)
        {
            var order = await unitOfWork.Orders.GetOrder(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return new OrderDTO
            {
                Id = order.Id,
                CourseId = order.CourseId,
                CourseName = order.Course.Title,
                OrderDate = order.OrderDate,
            };

        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserId(string userId)
        {
            var orders = await unitOfWork.Orders.GetOrdersByUserId(userId);
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CourseId = o.CourseId,
                CourseName = o.Course.Title,
                OrderDate = o.OrderDate,
            }).ToList();

        }
    }
}
