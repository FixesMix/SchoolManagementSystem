using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Provider.Interfaces;
using SchoolManagementSystem.Provider.Services;

namespace SchoolManagementSystem.Controllers
{
    public class FeesController : Controller
    {
        private readonly IFeeService _feeService;

        public FeesController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpPost("add-course-fee")]
        public async Task<IActionResult> AddCourseFee(string courseId, decimal cost)
        {
            var result = await _feeService.AddCourseFee(courseId, cost);
            return Ok(result);
        }

        [HttpPost("assign-student-fee")]
        public async Task<IActionResult> AssignStudentFee([FromBody]FeesRequest studentFee)
        {
            var result = await _feeService.AssignStudentFee(studentFee);
            return Ok(result);
        }

        [HttpGet("get-Student-Fees")] //no body needed cus youre just retrieving the results
        public async Task<IActionResult> GetStudentFee(string studentId)
        {
            var result = await _feeService.GetStudentFee(studentId);
            return Ok(result);
        }

        [HttpPut("update-student-fee")]
        public async Task<IActionResult> UpdateStudentFee([FromBody]UpdateFeeRequest studentFee)
        {
            var result = await _feeService.UpdateStudentFee(studentFee);
            return Ok(result);  
        }

    }
}
