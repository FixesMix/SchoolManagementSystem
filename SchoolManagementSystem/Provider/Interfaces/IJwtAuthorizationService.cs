using SchoolManagementSystem.DTOs.Users;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IJwtAuthorizationService
    {
        //modify this for student sign in

        Task<string?> GenerateToken(string email, string password);
        Task<User> AddUser(AddUserRequest newUser);
    }
}
