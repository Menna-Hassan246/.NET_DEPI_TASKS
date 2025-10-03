using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDBContext _context;

        public TaskRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
            => await _context.Tasks.Include(t => t.User).ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id)
            => await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId)
            => await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

        public async Task AddAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
  