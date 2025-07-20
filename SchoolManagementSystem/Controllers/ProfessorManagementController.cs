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

        [HttpGet ("display-professor")]
        public async Task <ActionResult<IEnumerable<ProfessorDTO>>> DisplayProfessors()
        {
            var result = await _professorManagementService.GetAllProfessorsAsync();
            return Ok(result);
        }

        [HttpGet("display-professor-by-id")]
        public async Task<ActionResult<IEnumerable<ProfessorDTO>>> DisplayStudentsById(string id)
        {
            var result = await _professorManagementService.GetProfessorByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("create-professor")]
        public async Task<IActionResult> CreateProfessor(string userId, [FromBody] CreateProfessorDTO dto)
        {
            var result = await _professorManagementService.CreateProfessorAsync(userId, dto);
            return Ok(result);
        }


        [HttpPut("update-professor")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateProfessorDTO dto, string id)
        {
            var result = await _professorManagementService.UpdateProfessorAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("delete-professor")]
        public async Task<IActionResult> DeleteProfessor(string id)
        {
            var result = await _professorManagementService.DeleteProfessorAsync(id);
            return Ok(result);
        }
    }
}
