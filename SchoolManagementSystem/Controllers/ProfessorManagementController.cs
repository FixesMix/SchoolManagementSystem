using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.Users.Professor;
using SchoolManagementSystem.DTOs.Users.Student;
using SchoolManagementSystem.Provider.Interfaces;
using SchoolManagementSystem.Provider.Services;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorManagementController : Controller
    {
        private readonly IProfessorManagementService _professorManagementService;
        public ProfessorManagementController(IProfessorManagementService professorManagementService)
        {
            _professorManagementService = professorManagementService;
        }

        [HttpGet ("Display Professor")]
        public async Task <ActionResult<IEnumerable<ProfessorDTO>>> DisplayProfessors()
        {
            var result = await _professorManagementService.GetAllProfessorsAsync();
            return Ok(result);
        }

        [HttpGet("Display Professor By Id")]
        public async Task<ActionResult<IEnumerable<ProfessorDTO>>> DisplayStudentsById(string id)
        {
            var result = await _professorManagementService.GetProfessorByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Create Professor")]
        public async Task<IActionResult> CreateProfessor(string userId, [FromBody] CreateProfessorDTO dto)
        {
            var result = await _professorManagementService.CreateProfessorAsync(userId, dto);
            return Ok(result);
        }


        [HttpPut("Update Professor")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateProfessorDTO dto, string id)
        {
            var result = await _professorManagementService.UpdateProfessorAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("Delete Professor")]
        public async Task<IActionResult> DeleteProfessor(string id)
        {
            var result = await _professorManagementService.DeleteProfessorAsync(id);
            return Ok(result);
        }
    }
}
