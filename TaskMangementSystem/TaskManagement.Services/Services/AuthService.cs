using Microsoft.AspNetCore.Identity;
using TaskManagement.Data.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        // Implements IAuthService.RegisterAsync(string, string, string)
        public async Task<User> RegisterAsync(string username, string password, string role)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
                throw new Exception("User already exists");

            if (!Enum.TryParse<UserRole>(role, out var userRole))
                throw new ArgumentException("Invalid role", nameof(role));

            var user = new User
            {
                UserName = username,
                Role = userRole
            };

            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                // Generate token here (implement GenerateJwtToken)
                user.Token = GenerateJwtToken(user);
                return user.Token;
            }

            return null;
        }
        private static string? GenerateJwtToken(User user)
        {
            return string.Empty;
        }
       

    }
}