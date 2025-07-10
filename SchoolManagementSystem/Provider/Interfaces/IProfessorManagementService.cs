using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.DTOs.Users.Professor;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IProfessorManagementService
    {
        Task<IEnumerable<ProfessorDTO>> GetAllProfessorsAsync();
        Task<ProfessorDTO?> GetProfessorByIdAsync(string userId);
        Task<ProfessorDTO> CreateProfessorAsync(string userId, CreateProfessorDTO dto);
        Task<ProfessorDTO?> UpdateProfessorAsync(string userId, UpdateProfessorDTO dto);
        Task<bool> DeleteProfessorAsync(string userId);
    }
}
