using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs.Users.Professor;
using SchoolManagementSystem.DTOs.Users.Student;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Provider.Services
{
    public class ProfessorManagementService : IProfessorManagementService
    {
        private readonly AppDbContext _dbContext;
        public ProfessorManagementService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProfessorDTO>> GetAllProfessorsAsync()
        {
            var professors = await _dbContext.Professors
                .Select(p => new ProfessorDTO
                {
                    UserId = p.UserId,
                    TaughtCourses = p.TaughtCourses,
                    Department = p.Department,
                    EmploymentDate = DateTime.UtcNow
                }).ToListAsync();

            return professors;
        }

        public async Task<ProfessorDTO?> GetProfessorByIdAsync(string userId)
        {
            var professors = await _dbContext.Professors
                .Where(p => p.UserId == userId)
                .Select(p => new ProfessorDTO
                {
                    UserId = p.UserId,
                    TaughtCourses = p.TaughtCourses,
                    Department = p.Department,
                    EmploymentDate = DateTime.UtcNow
                }).FirstOrDefaultAsync();

            return professors;
        }

        public async Task<ProfessorDTO> CreateProfessorAsync(string userId, CreateProfessorDTO dto)
        {
            var added = new Professor
            {
                UserId = userId,
                TaughtCourses = dto.TaughtCourses,
                Department = dto.Department,
                EmploymentDate = dto.EmploymentDate
            };

            _dbContext.Professors.Add(added);
            await _dbContext.SaveChangesAsync();

            var result = new ProfessorDTO
            {
                UserId = added.UserId,
                TaughtCourses = added.TaughtCourses,
                Department = added.Department,
                EmploymentDate = DateTime.UtcNow
            };

            return result;
        }

        public async Task<ProfessorDTO?> UpdateProfessorAsync(string userId, UpdateProfessorDTO dto)
        {
            var professor = await _dbContext.Professors.FindAsync(userId);
            if (professor == null)
                return null;

            professor.TaughtCourses = dto.TaughtCourses;
            professor.Department = dto.Department;
            professor.EmploymentDate = dto.EmploymentDate;

            await _dbContext.SaveChangesAsync();

            var result = new ProfessorDTO
            {
                UserId = professor.UserId,
                TaughtCourses = professor.TaughtCourses,
                Department = professor.Department,
                EmploymentDate = DateTime.UtcNow
            };

            return result;
        }
        
        public async Task<bool> DeleteProfessorAsync(string userId)
        {
            var selectedEntry = await _dbContext.Professors.FindAsync(userId);
            if (selectedEntry == null)
                return false;

            _dbContext.Professors.Remove(selectedEntry);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
