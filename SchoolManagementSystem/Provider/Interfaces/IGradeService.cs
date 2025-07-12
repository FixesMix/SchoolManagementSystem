using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IGradeService
    {
        Task AssignGrade(string studentId, string courseId, double score);
        Task<double?> GetGrade(string studentId, string courseId);
        Task<IEnumerable<Grade>> GetGradesForStudent(string studentId);
        Task<IEnumerable<Grade>> GetGradesForCourse(string courseId);
        Task<double> CalculateGPA(string studentId);

        Task UpdateGrade(string studentId, string courseId, double newScore);
        Task DeleteGrade(string studentId, string courseId);

    }
}
