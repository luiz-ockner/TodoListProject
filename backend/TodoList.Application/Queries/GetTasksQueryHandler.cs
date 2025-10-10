using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Queries
{
    // O handler implementa IRequestHandler<TRequest, TResponse>.
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();

            // Mapeamento: Entidade de Domínio (Task) -> DTO (TaskDto)
            return tasks.Select(t => new TaskDto(t.Id, t.Title, t.IsCompleted)).ToList();
        }
    }
}
