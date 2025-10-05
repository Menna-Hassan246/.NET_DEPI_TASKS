using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.App.DTOS
{
    public class ProjectDtos
    {

        public class ProjectCreateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class ProjectUpdateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class ProjectReadDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class ProjectDeleteDto
        {
            public int Id { get; set; }

        }
    }
}