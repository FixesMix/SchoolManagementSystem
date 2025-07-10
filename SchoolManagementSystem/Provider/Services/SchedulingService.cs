using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolManagementSystem.Provider.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly AppDbContext _dbContext;

        public SchedulingService(AppDbContext dbContext)
        {
          _dbContext = dbContext;
        }

        public async Task<ApiResponse<GetScheduledCourseResponse>> CreateSchedule(AddScheduledCoursesRequest courseSchedule)
        {
            //use classroomid to fetch data dfrom schedule to see fi already in use
            var schedules = await _dbContext.Schedules
                .Where(s => s.CourseId == courseSchedule.CourseId)
                .ToListAsync();

            //check endtime and start times and compare
            var classroomCheck = schedules.Any(s => (courseSchedule.StartTime >= s.StartTime //any returns a bool
                                                    && courseSchedule.EndTime <= s.EndTime)
                                                    || (courseSchedule.EndTime > s.StartTime && courseSchedule.EndTime <= s.EndTime)
                                                    || (courseSchedule.StartTime >= s.StartTime && courseSchedule.StartTime < s.EndTime)
                                                    || courseSchedule.StartTime <= s.StartTime && courseSchedule.EndTime >= s.EndTime);

            if (classroomCheck)
                return new ApiResponse<GetScheduledCourseResponse>
                {
                    Code = StatusCodes.Status424FailedDependency,
                    Message = "Classroom conflict with existing course."
                };

            var added = new Schedule
            {
                Id = Guid.NewGuid().ToString(), //pk pf schedule
                ClassroomId = courseSchedule.ClassroomId,
                CourseId = courseSchedule.CourseId,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow,
            };
            _dbContext.Schedules.Add(added);
            await _dbContext.SaveChangesAsync();

            //map to DTO
            var newSchedule = new GetScheduledCourseResponse
            {
                CourseId = added.CourseId,
                ClassroomId = added.ClassroomId,
                StartTime = added.StartTime,
                EndTime = added.EndTime,
            };
            
            return new ApiResponse<GetScheduledCourseResponse>
            {
                Code = StatusCodes.Status200OK,
                Message = "Successfully scheduled classroom for course.",
                Data = newSchedule
            };
            
        }

        public async Task<ApiResponse<string>> UpdateSchedule(string scheduleId, AddScheduledCoursesRequest updatedDetails)
        {
            var schedule = await _dbContext.Schedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return new ApiResponse<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Schedule not found."
                };
            }

            // check for time conflicts in the same classroom (not including current schedule)
            var overlapping = await _dbContext.Schedules
                .Where(s => s.Id != scheduleId && s.ClassroomId == updatedDetails.ClassroomId)
                .Where(s =>
                    (updatedDetails.StartTime >= s.StartTime && updatedDetails.StartTime < s.EndTime) ||
                    (updatedDetails.EndTime > s.StartTime && updatedDetails.EndTime <= s.EndTime) ||
                    (updatedDetails.StartTime <= s.StartTime && updatedDetails.EndTime >= s.EndTime))
                .AnyAsync();

            if (overlapping)
            {
                return new ApiResponse<string>
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Updated time conflicts with another schedule in the same classroom."
                };
            }

            //updating
            schedule.ClassroomId = updatedDetails.ClassroomId;
            schedule.CourseId = updatedDetails.CourseId;
            schedule.StartTime = updatedDetails.StartTime;
            schedule.EndTime = updatedDetails.EndTime;

            await _dbContext.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Code = StatusCodes.Status200OK,
                Message = "Schedule updated successfully."
            };
        }

        public async Task CancelSchedule(string scheduleId)
        {
            var result = await _dbContext.Schedules.FindAsync(scheduleId);
           
            _dbContext.Remove(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ApiResponse<List<GetScheduledCourseResponse>>> GetSchedulesByDate(DateTime date)
        {
            var foundSchedules = await _dbContext.Schedules
                .Where(s => s.StartTime.Date == date.Date)
                .Select(s => new GetScheduledCourseResponse
                {
                    ClassroomId = s.ClassroomId,
                    CourseId = s.CourseId,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                })
                .ToListAsync();  //since we're returning a list of schedules

            if (!foundSchedules.Any())
            {
                return new ApiResponse<List<GetScheduledCourseResponse>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "No schedules found for the date specified."
                };
            }

            return new ApiResponse<List<GetScheduledCourseResponse>>
            {
                Code = StatusCodes.Status200OK,
                Message = "Schedules found.",
                Data = foundSchedules
            };

        }
    }
}
