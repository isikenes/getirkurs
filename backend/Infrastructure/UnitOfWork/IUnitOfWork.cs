using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        Task<int> Save();
    }
}
