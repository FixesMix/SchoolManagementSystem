namespace SchoolManagementSystem.Models
{
    public class ProfessorCourse
    {
        public string ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
    }

}
