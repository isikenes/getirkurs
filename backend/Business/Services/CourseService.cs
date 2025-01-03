using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;

namespace Business.Services
{
    public class CourseService(IUnitOfWork unitOfWork) : ICourseService
    {

        public async Task Create(CourseDTO course)
        {
            var courseEntity = new Course
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Category = course.Category,
                Price = course.Price,
                ImageUrl = course.ImageUrl,
                Hours = course.Hours,
                InstructorId = course.InstructorId
            };
            await unitOfWork.Courses.Create(courseEntity);
            await unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            var course = unitOfWork.Courses.GetById(id);
            if (course != null)
            {
                await unitOfWork.Courses.Delete(id);
                await unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            var courses = await unitOfWork.Courses.GetAll();
            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Category = c.Category,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Hours = c.Hours,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor.DisplayName
            });
        }

        public async Task<CourseDTO> GetById(int id)
        {
            var course = await unitOfWork.Courses.GetById(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Category = course.Category,
                Price = course.Price,
                ImageUrl = course.ImageUrl,
                Hours = course.Hours,
                InstructorId = course.InstructorId,
                InstructorName = course.Instructor.DisplayName
            };
        }

        public async Task Update(CourseDTO course)
        {
            var courseEntity = await unitOfWork.Courses.GetById(course.Id);
            if (courseEntity != null)
            {
                courseEntity.Title = course.Title;
                courseEntity.Description = course.Description;
                courseEntity.Category = course.Category;
                courseEntity.Price = course.Price;
                courseEntity.ImageUrl = course.ImageUrl;
                courseEntity.Hours = course.Hours;
                courseEntity.InstructorId = course.InstructorId;

                await unitOfWork.Courses.Update(courseEntity);
                await unitOfWork.Save();
            }
        }

        async Task<IEnumerable<CourseDTO>> ICourseService.GetByCategory(string category)
        {
            var courses = await unitOfWork.Courses.GetByCategory(category);

            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Category = c.Category,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Hours = c.Hours,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor.DisplayName
            });
        }
    }
}
