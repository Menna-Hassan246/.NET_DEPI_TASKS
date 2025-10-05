using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Entities
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "ToDo";

        [StringLength(20)]
        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int? AssignedTo { get; set; }
        public User User { get; set; }
    }
}
