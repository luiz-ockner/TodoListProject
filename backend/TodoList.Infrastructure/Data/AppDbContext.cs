
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações básicas de modelo, se necessário
            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id);
        }
    }
}