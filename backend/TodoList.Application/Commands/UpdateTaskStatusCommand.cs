using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Commands
{
    public record UpdateTaskStatusCommand(int Id, bool IsCompleted):IRequest<bool>;   
}
