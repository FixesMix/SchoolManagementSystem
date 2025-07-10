using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.DTOs.Users;
using SchoolManagementSystem.Provider.Interfaces;
using SchoolManagementSystem.Provider.Services;

namespace SchoolManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IJwtAuthorizationService _jwtAuthorizationService;

        public LoginController(IJwtAuthorizationService jwtAuthorizationService)
        {
            _jwtAuthorizationService = jwtAuthorizationService;    
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] AddUserRequest newUser)
        {
            var addedUser = await _jwtAuthorizationService.AddUser(newUser);

            return Ok(addedUser);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login credentials)
        {
            var token = await _jwtAuthorizationService.GenerateToken(credentials.Email, credentials.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
