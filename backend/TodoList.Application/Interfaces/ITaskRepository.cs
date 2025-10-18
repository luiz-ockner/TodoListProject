
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetAllAsync();
        Task<Task> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Task task);
        System.Threading.Tasks.Task UpdateAsync(Task task);
    }
}
