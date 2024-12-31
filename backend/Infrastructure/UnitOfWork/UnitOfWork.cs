using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private ICourseRepository courseRepository;
        private IOrderRepository orderRepository;
        private IPaymentMethodRepository paymentMethodRepository;

        public ICourseRepository Courses
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(context);
                }
                return orderRepository;
            }
        }

        public IPaymentMethodRepository PaymentMethods
        {
            get
            {
                if (paymentMethodRepository == null)
                {
                    paymentMethodRepository = new PaymentMethodRepository(context);
                }
                return paymentMethodRepository;
            }
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
