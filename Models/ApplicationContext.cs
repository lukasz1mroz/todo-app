using Microsoft.EntityFrameworkCore;
using EntityFrameworkExercise.Models;


namespace EntityFrameworkExercise.Models
{
    // Class representation of DB and it's connection
    public class ApplicationContext : DbContext
    {
        public DbSet<Todo> Tasks { get; set; }  
        public DbSet<Asignee> Asignees { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignee>().HasMany(p => p.Tasks);
        }
    }
}
