using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface ISchedulingService
    {
        Task<ApiResponse<GetScheduledCourseResponse>> CreateSchedule(AddScheduledCoursesRequest courseSchedule);
        Task<ApiResponse<string>> UpdateSchedule(string scheduleId, AddScheduledCoursesRequest updatedDetails);
        Task CancelSchedule(string scheduleId);
        Task<ApiResponse<List<GetScheduledCourseResponse>>> GetSchedulesByDate(DateTime date);

    }
}
