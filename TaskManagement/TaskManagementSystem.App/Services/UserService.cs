using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.Reposatory;
using static TaskManagementSystem.App.DTOS.UserDtos;

namespace TaskManagementSystem.App.Services
{
    public class UserService : IUserInterface
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserReadDto>> GetAll()
        {
            var users = await _repo.GetAllAsync();

            // Manual mapping
            return users.Select(u => new UserReadDto
            {
                Id = u.UserId,
                Name = u.FullName,
                Email = u.Email,
                Role = u.Role
            });
        }

        public async Task<UserReadDto> GetById(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found");

            return new UserReadDto
            {
                Id = user.UserId,
                Name = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task Create(UserCreateDto dto)
        {
            var user = new User
            {
                FullName = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role
            };

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
        }

        public async Task Update(int id, UserUpdateDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found");
            user.FullName = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;


            _repo.Update(user);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found");

            _repo.Delete(user);
            await _repo.SaveChangesAsync();
        }
    }
}


