using SchoolManagementSystem.DTOs.Attendances;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IAttendanceService
    {
        Task<bool> AddAttendanceAsync(AddAttendance dto);
        Task<List<GetAttendance>> GetAttendanceByStudentAsync(string studentId);
        Task<List<GetAttendance>> GetAttendanceByCourseAsync(string courseId);
        Task<GetAttendance?> GetAttendanceRecordAsync(string studentId, string courseId, DateTime date);
        Task<bool> UpdateAttendanceAsync(string studentId, string courseId, DateTime date, UpdateAttendance updateDto);
    }
}
