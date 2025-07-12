namespace SchoolManagementSystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public double Score { get; set; } 
        public string LetterGrade {  get; set; }
        public DateTime DateRecorded { get; set; }


    }

}
