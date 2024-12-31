namespace Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int CourseId { get; set; }
        public DateTime OrderDate { get; set; }

        public Course Course { get; set; }
    }
}
