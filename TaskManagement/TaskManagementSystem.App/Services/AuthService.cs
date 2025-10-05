using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.Core.Entities;
using static TaskManagementSystem.App.Dtos.AuthDto;
using BCrypt.Net;

namespace TaskManagementSystem.App.Services
{
    public class AuthService : IAuthInterface
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Register(RegisterDto dto)
        {
            // check if user exists
            var existingUser = await _repo.GetByNameAsync(dto.Name);
            if (existingUser != null)
                throw new Exception("User already exists");

            // create user
            var user = new User
            {
                FullName = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Name = user.FullName,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Name = user.FullName,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim("name", user.FullName),
        new Claim("role", user.Role)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}