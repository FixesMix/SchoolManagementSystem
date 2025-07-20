using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;
using SchoolManagementSystem.Provider.Services;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulingController : Controller
    {
        private readonly ISchedulingService _schedulingService;
        public SchedulingController(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody]AddScheduledCoursesRequest courseSchedule)
        {
            var result = await _schedulingService.CreateSchedule(courseSchedule);
            return Ok(result);
        }

        [HttpPut("update-schedules")]
        public async Task<IActionResult> UpdateSchedule([FromBody] AddScheduledCoursesRequest updatedDetails, string scheduleId)
        {
            var result = await _schedulingService.UpdateSchedule(scheduleId, updatedDetails);
            return Ok(result);
        }

        [HttpDelete("delete-schedules")]
        public async Task<IActionResult> CancelSchedule(string scheduleId)
        {
            var result = _schedulingService.CancelSchedule(scheduleId);
            return Ok(result);
        }

        [HttpGet("retrieve-schedules")]
        public async Task<IActionResult> GetSchedulesByDate(DateTime date)
        {
            var result = await _schedulingService.GetSchedulesByDate(date);
            return Ok(result);
        }

    }
}
