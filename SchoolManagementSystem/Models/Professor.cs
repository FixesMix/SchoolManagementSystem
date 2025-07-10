using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Professor
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public List<string?> TaughtCourses { get; set; }
        public string? Department { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public User User { get; set; }

        public ICollection<ProfessorCourse> ProfessorCourses { get; set; } = new List<ProfessorCourse>();
    }
}
