﻿namespace Infrastructure.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public float Hours { get; set; }
        public int InstructorId { get; set; }
    }
}
