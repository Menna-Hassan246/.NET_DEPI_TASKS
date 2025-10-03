using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class User
    {
        
            public int Id { get; set; }
            public string UserName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string PasswordHash { get; set; } = null!;
            public UserRole Role { get; set; } = UserRole.User;
            public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public string? Token { get; set; }
      
    }
}
