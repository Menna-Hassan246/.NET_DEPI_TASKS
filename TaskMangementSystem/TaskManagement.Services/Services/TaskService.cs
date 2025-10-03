using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services.Services
{ public class TaskService : ITaskService
        {
            private readonly ITaskRepository _taskRepository;

            public TaskService(ITaskRepository taskRepository)
            {
                _taskRepository = taskRepository;
            }

            public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
                => await _taskRepository.GetAllAsync();

            public async Task<TaskItem?> GetTaskByIdAsync(int id)
                => await _taskRepository.GetByIdAsync(id);

            public async Task<IEnumerable<TaskItem>> GetUserTasksAsync(int userId)
                => await _taskRepository.GetTasksByUserIdAsync(userId);

            public async Task<TaskItem> CreateTaskAsync(TaskItem task)
            {
                await _taskRepository.AddAsync(task);
                return task;
            }

            public async Task<TaskItem?> UpdateTaskAsync(int id, TaskItem updatedTask)
            {
                var existingTask = await _taskRepository.GetByIdAsync(id);
                if (existingTask == null) return null;

                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.IsCompleted = updatedTask.IsCompleted;

                await _taskRepository.UpdateAsync(existingTask);
                return existingTask;
            }

      
            public async Task<TaskItem?> PatchTaskAsync(int id, string? title, string? description, bool? isCompleted)
            {
                var existingTask = await _taskRepository.GetByIdAsync(id);
                if (existingTask == null) return null;

                if (!string.IsNullOrEmpty(title)) existingTask.Title = title;
                if (!string.IsNullOrEmpty(description)) existingTask.Description = description;
                if (isCompleted.HasValue) existingTask.IsCompleted = isCompleted.Value;

                await _taskRepository.UpdateAsync(existingTask);
                return existingTask;
            }

            public async Task<bool> DeleteTaskAsync(int id)
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null) return false;

                await _taskRepository.DeleteAsync(task);
                return true;
            }
        }
    }


