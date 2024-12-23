using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public ICourseRepository Courses { get; }

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
