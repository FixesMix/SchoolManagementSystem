﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.Grades;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("assign-grade")]
        public async Task<IActionResult> AssignGrade([FromBody] GradingRequest request)
        {
            await _gradeService.AssignGrade(request.StudentId, request.CourseId, request.Score);
            return Ok("Grade assigned.");
        }

        [HttpGet("get-gpa")]
        public async Task<IActionResult> GetGPA(string studentId)
        {
            var gpa = await _gradeService.CalculateGPA(studentId);
            return Ok(gpa);
        }

        [HttpGet("get-list-of-a-students-grades")]
        public async Task<IActionResult> GetStudentGrades(string studentId)
        {
            var grades = await _gradeService.GetGradesForStudent(studentId);
            return Ok(grades);
        }

       
        [HttpGet("get-student-grade-in-1-course")]
        public async Task<IActionResult> GetGrade([FromQuery] string studentId, [FromQuery] string courseId)
        {
            var grade = await _gradeService.GetGrade(studentId, courseId);
            return Ok(grade);
        }

        [HttpPut("update-a-grade")]
        public async Task<IActionResult> UpdateGrade(string studentId, string courseId, double newScore)
        {
            await _gradeService.UpdateGrade(studentId, courseId, newScore);
            return Ok("Grade-updated.");
        }

        [HttpDelete("delete-a-grade")]
        public async Task<IActionResult> DeleteGrade(string studentId, string courseId)
        {
            await _gradeService.DeleteGrade(studentId, courseId);
            return Ok("Grade deleted.");
        }



    }
}

