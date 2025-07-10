using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        [Key] //marks UserID as PK
        [ForeignKey("User")] //Links to navigation property
        public string UserId { get; set; } 
        public DateTime GraduationDate { get; set; }
        public DateTime EnrollDate { get; set; }
        public int CompletedCredits { get; set; }
        public string? Advisor { get; set; }
        public User User { get; set; } // navigation property for foreign key relationship


        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
