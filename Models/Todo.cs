using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExercise.Models
{
    public class Todo
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueAt { get; set; } 
        [Required]
        public bool IsUrgent { get; set; } = false;
        [Required]
        public bool IsDone { get; set; } = false;
        public Asignee Asignee { get; set; }

        public Todo(string title, string description, bool isUrgent, bool isDone)
        {
            this.Title = title;
            this.Description = description;
            this.IsUrgent = isUrgent;
            this.IsDone = isDone;
            this.CreatedAt = DateTime.Now;
        }
    }
}
