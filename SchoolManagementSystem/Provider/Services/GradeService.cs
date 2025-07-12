using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Provider.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _dbContext;

        public GradeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string ConvertToLetterGrade(double score)
        {
            return score switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }

        public async Task AssignGrade(string studentId, string courseId, double score)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.CourseId == courseId);

            string letter = score >= 90 ? "A" : score >= 80 ? "B" : score >= 70 ? "C" : score >= 60 ? "D" : "F";

            if (grade == null)
            {
                grade = new Grade { StudentId = studentId, CourseId = courseId };
                _dbContext.Grades.Add(grade);
            }

            grade.Score = score;
            grade.LetterGrade = letter;
            grade.DateRecorded = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<double?> GetGrade(string studentId, string courseId)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.CourseId == courseId);

            return grade?.Score;
        }

        public async Task<IEnumerable<Grade>> GetGradesForStudent(string studentId) //gets grades for a single student
        {
            return await _dbContext.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesForCourse(string courseId) //get grades for entire course
        {
            return await _dbContext.Grades
                .Where(g => g.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<double> CalculateGPA(string studentId)
        {
            var grades = await GetGradesForStudent(studentId);

            double gpa = grades
            .Select(g => g.LetterGrade switch
            {
                "A" => 4.0,
                "B" => 3.0,
                "C" => 2.0,
                "D" => 1.0,
                _ => 0.0
            })
            .DefaultIfEmpty(0)
            .Average();

            return gpa;
        }


        public async Task UpdateGrade(string studentId, string courseId, double newScore)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.CourseId == courseId);

            if (grade == null)
                throw new Exception("Grade not found.");

            grade.Score = newScore;
            grade.LetterGrade = ConvertToLetterGrade(newScore);
            grade.DateRecorded = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteGrade(string studentId, string courseId)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.CourseId == courseId);

            if (grade == null)
                throw new Exception("Grade not found.");

            _dbContext.Grades.Remove(grade);
            await _dbContext.SaveChangesAsync();
        }


    }
}
