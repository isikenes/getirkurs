using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository(AppDbContext context) : ICourseRepository
    {
        public async Task Create(Course course)
        {
            await context.Courses.AddAsync(course);
        }

        public async Task Delete(int id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course != null)
                context.Courses.Remove(course);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByCategory(string category)
        {
            var courses = await context.Courses
            .Where(c => c.Category == category)
            .ToListAsync();

            return courses;
        }

        public async Task<Course> GetById(int id)
        {
            return await context.Courses.FindAsync(id);
        }

        public async Task Update(Course course)
        {
            var existingCourse = await context.Courses.FindAsync(course.Id);
            if (existingCourse != null)
                context.Entry(existingCourse).CurrentValues.SetValues(course);
        }
    }
}
