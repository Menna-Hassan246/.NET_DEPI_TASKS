using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Reposatory
{
    public class ProjectReposatory : IProjectRepository
    {
        private readonly  AppDbContext _context;
        public ProjectReposatory(AppDbContext context)
        {
            _context = context; 
        }
        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }


        public async Task<IEnumerable<Project>> GetAllAsync()
        {
           return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

          public void Delete(Project project)
        {
            _context.Projects.FindAsync(project.ProjectId);
            _context.Projects.Remove(project);

        }
    }
}
