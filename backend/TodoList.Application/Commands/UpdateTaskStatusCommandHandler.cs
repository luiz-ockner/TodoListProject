using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Commands
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        public UpdateTaskStatusCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {

            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                return false; // Tarefa não encontrada
            }

            if(request.IsCompleted)
            {
                task.MarkAsCompleted();
            }
            else
            {
                task.IsCompleted = false; // Marca como não concluída
            }

            await _taskRepository.UpdateAsync(task);

            return true; // Atualização bem-sucedida
        }
    }
}
