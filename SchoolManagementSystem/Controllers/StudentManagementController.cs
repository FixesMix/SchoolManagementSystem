using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs.Users.Student;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentManagementController : Controller
    {
        private readonly IStudentManagementService _studentManagementService;
        public StudentManagementController(IStudentManagementService studentManagementService)
        {
            _studentManagementService = studentManagementService;
        }

        [HttpGet("display student")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> DisplayStudents()
        {
            var result = await _studentManagementService.GetAllStudentsAsync();
            return Ok(result);
        }

        [HttpGet("display student by id")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> DisplayStudentsById(string id)
        {
            var result = await _studentManagementService.GetStudentByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("create student")]
        public async Task<IActionResult> CreateStudent(string userId, [FromBody] CreateStudentDTO dto)
        {
            var result = await _studentManagementService.CreateStudentAsync(userId, dto);
            return Ok(result);
        }


        [HttpPut("update student")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDTO dto, string id)
        {
            var result = await _studentManagementService.UpdateStudentAsync(id, dto);
            return Ok(result);
        }


        [HttpDelete("delete student")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var result = await _studentManagementService.DeleteStudentAsync(id);
            return Ok(result);
        }

        [HttpPut("assign advisor")]
        public async Task<IActionResult> AssignAdvisor(string id, string advisor)
        {
            var updatedStudent = await _studentManagementService.AssignAdvisor(id, advisor);
            return Ok(updatedStudent);
        }

        [HttpPost("enroll student in course")]
        public async Task<IActionResult> EnrollStudentInCourse(string studentId, string courseId)
        {
            try
            {
                await _studentManagementService.EnrollStudentInCourse(studentId, courseId);
                return Ok($"Student {studentId} enrolled in course {courseId}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("drop student from course")]
        public async Task<IActionResult> DropStudentFromCourse(string studentId, string courseId)
        {
            await _studentManagementService.DropStudentFromCourse(studentId, courseId);
            return Ok();
        }

    }
}
