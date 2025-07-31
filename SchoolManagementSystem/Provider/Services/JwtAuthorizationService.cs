using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs.Users;
using SchoolManagementSystem.Helpers;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystem.Provider.Services
{
    public class JwtAuthorizationService : IJwtAuthorizationService
    {

        private readonly JwtConfig _jwtConfig;
        private readonly AppDbContext _dbContext;
        public JwtAuthorizationService(IOptions<JwtConfig> jwtOptions, AppDbContext dbcontext)
        {
            _jwtConfig = jwtOptions.Value;
            _dbContext = dbcontext;
        }

        public async Task<User> AddUser(AddUserRequest newUser)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var password = AuthenticationHelperService.passwordHash(newUser.Password, salt);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = newUser.Username,
                Password = password,
                Salt = salt,
                Role = newUser.Role,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);

            if (user.Role == "student")
            {
                var student = new Student
                {
                    UserId = user.Id, // PK = FK to User
                    EnrollDate = DateTime.UtcNow,
                    GraduationDate = DateTime.UtcNow.AddYears(4),
                    //EnrolledCourses = new List<string>(),
                    CompletedCredits = 0,
                    StudentType = "Part Time"
                };
                _dbContext.Students.Add(student);
            }
            else if (user.Role == "professor")
            {
                var professor = new Professor
                {
                    UserId = user.Id,
                    Department = "Undeclared",
                    TaughtCourses = new List<string>()
                };
                _dbContext.Professors.Add(professor);
            }

            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<string?> GenerateToken(string email, string password)
        {

            //assuming user in database trying to login
            //while creating user, password hashed
            //retreive the user first with email, gwt hashed pass, do verifications
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            var isUser = AuthenticationHelperService.VerifyPassword(password, user.Password, user.Salt);
            if (!isUser)
                return null;


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim("role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiresInMinutes),
                signingCredentials: creds
            );

            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
