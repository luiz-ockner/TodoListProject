using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Commands
{
    // O Command carrega os dados e define a resposta (neste caso, o ID da nova tarefa)
    public record CreateTaskCommand(string Title) : IRequest<int>;
}
