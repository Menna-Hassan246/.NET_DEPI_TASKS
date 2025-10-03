using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetUserTasksAsync(int userId);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(int id, TaskItem updatedTask);
        Task<TaskItem?> PatchTaskAsync(int id, string? title, string? description, bool? isCompleted);
        Task<bool> DeleteTaskAsync(int id);

    }
}
