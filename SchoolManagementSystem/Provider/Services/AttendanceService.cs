using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DTOs.Attendances;
using SchoolManagementSystem.Provider.Interfaces;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Provider.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _dbContext;

        public AttendanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAttendanceAsync(AddAttendance dto)
        {
            var attendance = new Attendance
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Date = dto.Date,
                IsPresent = dto.IsPresent
            };

            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetAttendance>> GetAttendanceByStudentAsync(string studentId)
        {
            var records = await _dbContext.Attendances
                .Where(a => a.StudentId == studentId)
                .ToListAsync();

            var result = records.Select(a => new GetAttendance
            {
                StudentId = a.StudentId,
                CourseId = a.CourseId,
                Date = a.Date,
                IsPresent = a.IsPresent
            }).ToList();

            return result;
        }

        public async Task<List<GetAttendance>> GetAttendanceByCourseAsync(string courseId)
        {
            var records = await _dbContext.Attendances
               .Where(a => a.CourseId == courseId)
               .ToListAsync();

            var result = records.Select(a => new GetAttendance
            {
                StudentId = a.StudentId,
                CourseId = a.CourseId,
                Date = a.Date,
                IsPresent = a.IsPresent
            }).ToList();

            return result;
        }

        public async Task<GetAttendance?> GetAttendanceRecordAsync(string studentId, string courseId, DateTime date)
        {
            var record = await _dbContext.Attendances
                .FirstOrDefaultAsync(a =>
                    a.StudentId == studentId &&
                    a.CourseId == courseId &&
                    a.Date.Date == date.Date);

            if (record == null)
                return null;

            return new GetAttendance
            {
                StudentId = record.StudentId,
                CourseId = record.CourseId,
                Date = record.Date,
                IsPresent = record.IsPresent
            };
        }

        public async Task<bool> UpdateAttendanceAsync(string studentId, string courseId, DateTime date, UpdateAttendance updateDto)
        {
            var attendance = await _dbContext.Attendances
                .FirstOrDefaultAsync(a =>
                    a.StudentId == studentId &&
                    a.CourseId == courseId &&
                    a.Date.Date == date.Date);

            if (attendance == null)
                return false;

            attendance.IsPresent = updateDto.IsPresent;
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
