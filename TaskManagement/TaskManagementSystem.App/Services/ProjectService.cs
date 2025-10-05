using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.Core.Entities;
using static TaskManagementSystem.App.DTOS.ProjectDtos;

namespace TaskManagementSystem.App.Services
{
    public class ProjectService : IProjectInterface
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

       
        public IEnumerable<ProjectReadDto> GetAll()
        {
            var projects = _repo.GetAllAsync().Result;
            return projects.Select(p => new ProjectReadDto
            {
                Id = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.CreatedAt,
                EndDate = p.CreatedAt
            });
        }

        public ProjectReadDto GetById(int id)
        {
            var project = _repo.GetByIdAsync(id).Result;
            if (project == null) return null;

            return new ProjectReadDto
            {
                Id = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.CreatedAt,
                EndDate = project.CreatedAt
            };
        }

        public void Create(ProjectCreateDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = dto.StartDate
            };
            _repo.AddAsync(project);
        }

        public void Update(int id, ProjectUpdateDto dto)
        {
            var projectTask = _repo.GetByIdAsync(id);
            var project = projectTask.Result;
            if (project == null) return;

            project.Name = dto.Name;
            project.Description = dto.Description;
            _repo.Update(project);
        }

        public void Delete(int id)
        {
            var projectTask = _repo.GetByIdAsync(id);
            var project = projectTask.Result;
            if (project == null) return;
            _repo.Delete(project);
        }
    }
}
