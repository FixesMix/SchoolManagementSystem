namespace SchoolManagementSystem.DTOs
{
    public class GetScheduledCourseResponse
    {   
        public string ClassroomId { get; set; }
        public string CourseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
