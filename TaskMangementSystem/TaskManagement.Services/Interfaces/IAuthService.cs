using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string username, string password, string role);
        Task<string> LoginAsync(string username, string password);
    }
}
