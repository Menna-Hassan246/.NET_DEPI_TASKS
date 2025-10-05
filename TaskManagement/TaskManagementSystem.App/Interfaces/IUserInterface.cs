using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.App.DTOS.UserDtos;

namespace TaskManagementSystem.App.Interfaces
{
    public interface IUserInterface
    {
       Task <IEnumerable<UserReadDto>> GetAll();
    Task  <UserReadDto> GetById(int id);
      Task Create(UserCreateDto dto);
       Task Update(int id, UserUpdateDto dto);
       Task Delete(int id);
    }
}
