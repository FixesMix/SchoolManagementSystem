using SchoolManagementSystem.DTOs.Users.Student;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IStudentManagementService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO?> GetStudentByIdAsync(string userId);
        Task<StudentDTO> CreateStudentAsync(string userId, CreateStudentDTO dto);
        Task<StudentDTO?> UpdateStudentAsync(string userId, UpdateStudentDTO dto);
        Task<bool> DeleteStudentAsync(string userId);

        Task<StudentDTO> AssignAdvisor(string userId, string advisor);
        Task EnrollStudentInCourse(string userId, string courseId);
        Task DropStudentFromCourse(string studentId, string courseId);

    }
}
