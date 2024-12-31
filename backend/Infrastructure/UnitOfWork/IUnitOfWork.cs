using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IOrderRepository Orders { get; }
        Task<int> Save();
    }
}
