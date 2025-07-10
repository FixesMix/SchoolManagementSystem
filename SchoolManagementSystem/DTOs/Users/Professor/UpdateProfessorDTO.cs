using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.DTOs.Users.Professor
{
    public class UpdateProfessorDTO
    {
        public List<string> TaughtCourses { get; set; }
        public string Department { get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
