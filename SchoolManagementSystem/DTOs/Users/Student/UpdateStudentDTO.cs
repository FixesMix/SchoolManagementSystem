namespace SchoolManagementSystem.DTOs.Users.Student
{
    public class UpdateStudentDTO
    {
        public DateTime EnrollDate { get; set; }
        public DateTime GraduationDate { get; set; }
        public int CompletedCredits { get; set; }
        public string? Advisor { get; set; }
        public List<string?> EnrolledCourses { get; set; }
        public int StudentLevel { get; set; }

    }
}
