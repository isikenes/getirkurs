using Business.DTOs;

namespace Business.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetAll();
        Task<CourseDTO> GetById(int id);
        Task Create(CourseDTO course);
        Task Update(CourseDTO course);
        Task Delete(int id);
    }
}
