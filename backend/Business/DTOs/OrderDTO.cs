namespace Business.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
