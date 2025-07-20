namespace SchoolManagementSystem.DTOs.Attendances
{
    public class AddAttendance
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
