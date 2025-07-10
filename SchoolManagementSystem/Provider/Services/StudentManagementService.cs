using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs.Users.Student;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Provider.Services
{
    public class StudentManagementService : IStudentManagementService
    {
        private readonly AppDbContext _dbContext;

        public StudentManagementService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentDTO> AssignAdvisor(string userId, string advisor)
        {
            var student = await _dbContext.Students.FindAsync(userId);
                if (student == null)
                    throw new Exception("Student not found.");
                
            student.Advisor = advisor;
            await _dbContext.SaveChangesAsync();

            var result = new StudentDTO
            {
                UserId = student.UserId,
                Advisor = student.Advisor,
                EnrollDate = student.EnrollDate,
                GraduationDate = student.GraduationDate,
                CompletedCredits = student.CompletedCredits
            };

            return result;
        }

        public async Task EnrollStudentInCourse(string studentId, string courseId) //relational table
        {
            var student = await _dbContext.Students
                .Include(s => s.StudentCourses) //include a list of all student courses the student has
                .FirstOrDefaultAsync(s => s.UserId == studentId); //inputted student id is = to student table's UserId

            if (student == null)
                throw new Exception("Student not found.");

            var course = await _dbContext.Courses.FindAsync(courseId);
            if (course == null)
                throw new Exception("Course not found.");

           
            if (student.StudentCourses.Any(sc => sc.CourseId == courseId)) //check if they're already enrolled
                throw new Exception("Student already enrolled in course.");

            // add new link to relational table using inserted values in student table and course table 
            student.StudentCourses.Add(new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task DropStudentFromCourse(string studentId, string courseId) //relational table
        {
            var studentCourse = await _dbContext.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (studentCourse == null)
                throw new Exception("student not enrolled");

            _dbContext.StudentCourses.Remove(studentCourse);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _dbContext.Students
                .Select(s => new StudentDTO
                {
                    UserId = s.UserId,
                    GraduationDate = s.GraduationDate,
                    EnrollDate = s.EnrollDate,
                    CompletedCredits = s.CompletedCredits
                }).ToListAsync();

            return students;
        }

        public async Task<StudentDTO?> GetStudentByIdAsync(string userId)
        {
            var student = await _dbContext.Students
                .Where(s => s.UserId == userId)
                .Select(s => new StudentDTO
                {
                    UserId = s.UserId,
                    GraduationDate = s.GraduationDate,
                    EnrollDate = s.EnrollDate,
                    CompletedCredits = s.CompletedCredits
                })
                .FirstOrDefaultAsync();

            return student;
        }

        public async Task<StudentDTO> CreateStudentAsync(string userId, CreateStudentDTO dto)
        {
            var added = new Student
            {
                UserId = userId,
                GraduationDate = dto.GraduationDate,
                EnrollDate = dto.EnrollDate,
                CompletedCredits = dto.CompletedCredits
            };

            _dbContext.Students.Add(added);
            await _dbContext.SaveChangesAsync();

            var result = new StudentDTO
            {
                UserId = added.UserId,
                GraduationDate = added.GraduationDate,
                EnrollDate = added.EnrollDate,
                CompletedCredits = added.CompletedCredits
            };

            return result;
        }

        public async Task<StudentDTO?> UpdateStudentAsync(string userId, UpdateStudentDTO dto)
        {
            var student = await _dbContext.Students.FindAsync(userId);
            if (student == null)
                return null;

            student.GraduationDate = dto.GraduationDate;
            student.EnrollDate = dto.EnrollDate;
            student.CompletedCredits = dto.CompletedCredits;

            await _dbContext.SaveChangesAsync();

            var result = new StudentDTO
            {
                UserId = student.UserId,
                GraduationDate = student.GraduationDate,
                EnrollDate = student.EnrollDate,
                CompletedCredits = student.CompletedCredits
            };

            return result;
        }

        public async Task<bool> DeleteStudentAsync(string userId)
        {
            var selectedEntry = await _dbContext.Students.FindAsync(userId);
            if (selectedEntry == null)
                return false;

            _dbContext.Students.Remove(selectedEntry);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
