namespace SchoolManagementSystem.Models
{
    public class CourseFees
    {
        public Guid Id { get; set; }
        public string CourseId { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
