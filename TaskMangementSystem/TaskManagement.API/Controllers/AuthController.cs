using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string role)
        {
            try
            {
                var user = await _authService.RegisterAsync(username, password, role.ToString());
                return Ok(new { message = "User registered successfully", user = user.UserName, role = user.Role.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _authService.LoginAsync(username, password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            // If user is a string, just return the username. Otherwise, access properties.
            if (user is string userName)
                return Ok(new { message = "Login successful", user = userName });

            // Use reflection to access properties only if they exist and user is not a string
            var userType = user.GetType();
            var userNameProp = userType.GetProperty("UserName");
            var roleProp = userType.GetProperty("Role");

            return Ok(new
            {
                message = "Login successful",
                user = userNameProp != null ? userNameProp.GetValue(user)?.ToString() : user.ToString(),
                role = roleProp != null ? roleProp.GetValue(user)?.ToString() : null
            });
        }
    }
}

