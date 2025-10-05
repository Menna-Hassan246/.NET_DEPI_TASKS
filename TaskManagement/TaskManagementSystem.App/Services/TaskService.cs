using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.App.DTOS;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.Core.Entities;
using static TaskManagementSystem.App.DTOS.TaskDtos;

namespace TaskManagementSystem.App.Services
{
    public class TaskService : ITaskInterface
    {
        private readonly ITaskRepository _repo;
        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public void Create(TaskCreateDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = "New",
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId,
                AssignedTo = dto.AssignedToId
            };
            _repo.AddAsync(task);
        }

        public void Delete(int id)
        {
            var task = _repo.GetByIdAsync(id).Result;
            if (task != null)
            {
                _repo.Delete(task);
            }
        }

        public IEnumerable<TaskReadDto> GetAll()
        {
            var tasks = _repo.GetAllAsync().Result;
            return tasks.Select(task => new TaskReadDto
            {
                Id = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate ?? default(DateTime),
                ProjectId = task.ProjectId,
                AssignedToId = task.AssignedTo ?? 0
            }).ToList();
        }

        public TaskReadDto GetById(int id)
        {
            var taskResult = _repo.GetByIdAsync(id);
            var task = taskResult.Result;
            if (task == null)
                return null;

            return new TaskReadDto
            {
                Id = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate ?? default(DateTime),
                ProjectId = task.ProjectId,
                AssignedToId = task.AssignedTo ?? 0
            };
        }

        public void Update(int id, TaskUpdateDto dto)
        {
            var taskResult = _repo.GetByIdAsync(id);
            var task = taskResult.Result;
            if (task != null)
            {
                task.Title = dto.Title;
                task.Description = dto.Description;
                task.Status = dto.Status;
                task.DueDate = dto.DueDate;
            }
        }
    }
}