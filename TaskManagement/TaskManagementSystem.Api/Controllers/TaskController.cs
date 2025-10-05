using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.App.Services;
using static TaskManagementSystem.App.DTOS.TaskDtos;

namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskInterface _service;

        public TaskController(ITaskInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _service.GetById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create(TaskCreateDto dto)
        {
            _service.Create(dto);
            return Ok("Task created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskUpdateDto dto)
        {
            _service.Update(id, dto);
            return Ok("Task updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Task deleted successfully");
        }
    }
}
