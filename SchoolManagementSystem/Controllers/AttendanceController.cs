using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.Attendances;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("get-attendance-records")]
        public async Task<IActionResult> GetAttendanceRecord(string studentId, string courseId, DateTime date)
        {
            var record = await _attendanceService.GetAttendanceRecordAsync(studentId, courseId, date);
            if (record == null) return NotFound("Attendance record not found.");
            return Ok(record);
        }

        [HttpGet("get-attendance-by-student")]
        public async Task<IActionResult> GetAttendanceByStudent(string studentId)
        {
            var records = await _attendanceService.GetAttendanceByStudentAsync(studentId);
            return Ok(records);
        }

        [HttpGet("get-attendance-by-course")]
        public async Task<IActionResult> GetAttendanceByCourse(string courseId)
        {
            var records = await _attendanceService.GetAttendanceByCourseAsync(courseId);
            return Ok(records);
        }


        [HttpPost("add-attendance")]
        public async Task<IActionResult> AddAttendance([FromBody] AddAttendance dto)
        {
            var success = await _attendanceService.AddAttendanceAsync(dto);
            if (!success) return BadRequest("failed to add");
            return Ok();
        }

        [HttpPut("update-attendance")]
        public async Task<IActionResult> UpdateAttendance(string studentId, string courseId, DateTime date, [FromBody] UpdateAttendance updateDto)
        {
            var success = await _attendanceService.UpdateAttendanceAsync(studentId, courseId, date, updateDto);
            if (!success) return NotFound("not found");
            return Ok();
        }

        


    }
}
