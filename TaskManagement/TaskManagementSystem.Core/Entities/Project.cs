using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int CreatedBy { get; set; }
        public User User { get; set; }

        // Relations
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
