namespace Business.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public float Hours { get; set; }

        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}
