using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.App.DTOS.TaskDtos;

namespace TaskManagementSystem.App.Interfaces
{
    public interface ITaskInterface
    {
        IEnumerable<TaskReadDto> GetAll();
        TaskReadDto GetById(int id);
        void Create(TaskCreateDto dto);
        void Update(int id, TaskUpdateDto dto);
        void Delete(int id);
    }

}

