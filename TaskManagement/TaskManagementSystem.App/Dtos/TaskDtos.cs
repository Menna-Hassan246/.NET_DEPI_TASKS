using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.App.DTOS
{
    public class TaskDtos
    {
     
        public class TaskCreateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public int ProjectId { get; set; }
            public int AssignedToId { get; set; }
        }

        public class TaskUpdateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public DateTime DueDate { get; set; }
        }

        public class TaskReadDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public DateTime DueDate { get; set; }
            public int ProjectId { get; set; }
            public int AssignedToId { get; set; }
        }
        public class TaskDeleteDto
        {
            public int Id { get; set; }
        }
    }

}



