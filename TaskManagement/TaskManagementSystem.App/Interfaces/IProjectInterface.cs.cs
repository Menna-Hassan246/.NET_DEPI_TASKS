using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.App.DTOS.ProjectDtos;

namespace TaskManagementSystem.App.Interfaces
{
    public interface IProjectInterface
    {
        IEnumerable<ProjectReadDto> GetAll();
        ProjectReadDto GetById(int id);
        void Create(ProjectCreateDto dto);
        void Update(int id, ProjectUpdateDto dto);
        void Delete(int id);
    }
}
