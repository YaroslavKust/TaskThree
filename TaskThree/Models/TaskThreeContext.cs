using Microsoft.EntityFrameworkCore;

namespace TaskThree.Models
{
    class TaskThreeContext: DbContext
    {
        public DbSet<Record> Records { get; set; }

        public TaskThreeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=appdb;Trusted_Connection=True;");
        }
    }
}
