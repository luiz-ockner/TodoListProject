using MediatR;

namespace TodoList.Application.Commands
{
    public record DeleteTaskCommand(int Id) : IRequest<bool>;
}
