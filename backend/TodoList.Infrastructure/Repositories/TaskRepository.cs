using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Data;
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync(); // Persiste a mudança no banco
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllAsync()
        {
            // Usando ToListAsync para buscar todos os registros
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> GetByIdAsync(int id)
        {
           return await _context.Tasks.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async System.Threading.Tasks.Task UpdateAsync(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
