using MediatR;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                return false; // Tarefa não encontrada
            }

            await _taskRepository.DeleteAsync(task);

            return true; // Tarefa deletada com sucesso
        }
    }
}
