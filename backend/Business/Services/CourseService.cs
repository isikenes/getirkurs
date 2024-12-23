﻿using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;

namespace Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public async Task Create(CourseDTO course)
        {
            var courseEntity = new Course
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
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
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Hours = c.Hours,
                InstructorId = c.InstructorId
            });
        }

        public async Task<CourseDTO> GetById(int id)
        {
            var course = await unitOfWork.Courses.GetById(id);

            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                ImageUrl = course.ImageUrl,
                Hours = course.Hours,
                InstructorId = course.InstructorId
            };
        }

        public async Task Update(CourseDTO course)
        {
            var courseEntity = await unitOfWork.Courses.GetById(course.Id);
            if (courseEntity != null)
            {
                courseEntity.Title = course.Title;
                courseEntity.Description = course.Description;
                courseEntity.Price = course.Price;
                courseEntity.ImageUrl = course.ImageUrl;
                courseEntity.Hours = course.Hours;
                courseEntity.InstructorId = course.InstructorId;

                await unitOfWork.Courses.Update(courseEntity);
                await unitOfWork.Save();
            }
        }
    }
}
