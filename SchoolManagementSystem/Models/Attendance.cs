using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
