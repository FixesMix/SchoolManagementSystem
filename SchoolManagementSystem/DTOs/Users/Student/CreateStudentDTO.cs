namespace SchoolManagementSystem.DTOs.Users.Student
{
    public class CreateStudentDTO
    {
        public string UserId { get; set; }
        public DateTime GraduationDate { get; set; }
        public DateTime EnrollDate { get; set; }
        public int CompletedCredits { get; set; }


    }
}
