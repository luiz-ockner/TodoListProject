using MediatR;
using TodoList.Application.DTOs;

namespace TodoList.Application.Queries
{
    // A query apenas pede uma lista de TaskDto. Implementa IRequest<TResponse>.
    public record GetTasksQuery : IRequest<List<TaskDto>>;
}
