using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.DTOs.Users.Professor
{
    public class ProfessorDTO
    {
        public string UserId { get; set; }
        public List<string> TaughtCourses { get; set; }
        public string Department { get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
