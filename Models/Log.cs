using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExercise.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public string HttpContent { get; set; }
        public string LogLevel { get; set; }
        public DateTime CreatedAt { get; set; }

        public Log(string path, string httpMethod, string httpContent, string logLevel)
        {
            this.Path = path;
            this.HttpMethod = httpMethod;
            this.HttpContent = httpContent;
            this.LogLevel = logLevel;
            this.CreatedAt = DateTime.Now;
        }
    }
}
