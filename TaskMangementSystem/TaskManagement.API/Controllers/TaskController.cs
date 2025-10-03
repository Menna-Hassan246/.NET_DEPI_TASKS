using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTasks(int userId)
        {
            var tasks = await _taskService.GetUserTasksAsync(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            var created = await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem task)
        {
            var updated = await _taskService.UpdateTaskAsync(id, task);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // PATCH → update part of entity
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] dynamic body)
        {
            string? title = body.title;
            string? description = body.description;
            bool? isCompleted = body.isCompleted;

            var updated = await _taskService.PatchTaskAsync(id, title, description, isCompleted);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

