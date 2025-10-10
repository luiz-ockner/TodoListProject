
namespace TodoList.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        // O construtor privado ou sem parâmetros é bom para o Entity Framework
        private Task() { }

        // Construtor para facilitar a criação de novas tarefas
        public Task(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title cannot be empty.");

            Title = title;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
}