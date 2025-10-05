using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.App.Services;
using static TaskManagementSystem.App.DTOS.ProjectDtos;

namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        
            private readonly IProjectInterface _service;

            public ProjectController(IProjectInterface service)
            {
                _service = service;
            }

            [HttpGet]
            public IActionResult GetAll() => Ok(_service.GetAll());

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var project = _service.GetById(id);
                if (project == null) return NotFound();
                return Ok(project);
            }

            [HttpPost]
            public IActionResult Create(ProjectCreateDto dto)
            {
                _service.Create(dto);
                return Ok("Project created successfully");
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, ProjectUpdateDto dto)
            {
                _service.Update(id, dto);
                return Ok("Project updated successfully");
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                _service.Delete(id);
                return Ok("Project deleted successfully");
            }
        }
}
