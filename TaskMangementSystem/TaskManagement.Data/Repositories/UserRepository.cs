using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context) => _context = context;

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.Include(u => u.Tasks).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    
    }
}
