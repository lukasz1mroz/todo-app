using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExercise.Models
{
    public class Asignee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Todo> Tasks { get; set; }

        public Asignee()
        {
        }
        public Asignee(string name)
        {
            this.Name = name;
        }

        public Asignee(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
    }
}
