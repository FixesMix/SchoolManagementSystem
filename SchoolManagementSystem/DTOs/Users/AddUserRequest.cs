using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.DTOs.Users
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        [RegularExpression("^(professor|student)$", ErrorMessage = "Role must be either 'professor' or 'student'.")]
        public string Role { get; set; }
    }
}
