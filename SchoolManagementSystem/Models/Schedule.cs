namespace SchoolManagementSystem.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public string ClassroomId { get; set; }
        public string CourseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
