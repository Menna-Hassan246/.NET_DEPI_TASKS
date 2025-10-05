using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.App.DTOS
{
    public class UserDtos
    {
        public class UserCreateDto
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }   
            public string Role { get; set; } = "User";
        }

        public class UserUpdateDto
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public class UserReadDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
      
        public class UserDeleteDto
        {
            public int Id { get; set; }
        }
    }

}

