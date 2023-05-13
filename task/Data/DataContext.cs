using Microsoft.EntityFrameworkCore;
using task.Models;

namespace task.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<Person> Persons { get; set; }
        
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasIndex(p => new { p.userName})
            .IsUnique(true);
    }
         
    }
}