using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.DTOs.Users.Student
{
    public class StudentDTO
    {
        public string UserId { get; set; }
        public DateTime GraduationDate { get; set; }
        public DateTime EnrollDate { get; set; }
        public int CompletedCredits { get; set; }
        public string? Advisor { get; set; }

        public int StudentLevel { get; set; }
        //public List<string> EnrolledCourses { get; set; } = new List<string>();
    }
}
