using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.App.Dtos.AuthDto;

namespace TaskManagementSystem.App.Interfaces
{
   public interface IAuthInterface
    {
        Task<AuthResponseDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}
