using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.App.Interfaces;
using TaskManagementSystem.App.Services;
using static TaskManagementSystem.App.DTOS.UserDtos;

namespace TaskManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _service;

        public UserController(IUserInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _service.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(UserCreateDto dto)
        {
            _service.Create(dto);
            return Ok("User created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserUpdateDto dto)
        {
            _service.Update(id, dto);
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("User deleted successfully");
        }
    }
}
