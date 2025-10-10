using MediatR;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            // 1. Cria a entidade de Domínio
            var newTask = new TodoList.Domain.Entities.Task(request.Title);

            // 2. Persiste a entidade usando o Repositório
            await _taskRepository.AddAsync(newTask);

            // 3. Retorna o ID da nova entidade
            return newTask.Id;
        }
    }
}
