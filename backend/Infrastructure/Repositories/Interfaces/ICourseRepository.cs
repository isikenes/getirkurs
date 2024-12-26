using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
        Task Create(Course course);
        Task Update(Course course);
        Task Delete(int id);
        Task<IEnumerable<Course>> GetByCategory(string category);
    }
}
