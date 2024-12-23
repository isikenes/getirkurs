using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await courseService.GetAll();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await courseService.GetById(id);
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDTO course)
        {
            await courseService.Create(course);
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseDTO course)
        {
            await courseService.Update(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await courseService.Delete(id);
            return NoContent();
        }
    }
}
