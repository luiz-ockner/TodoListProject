using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs
{
    // Record para a requisição de criação (o que o usuário envia)
    public record CreateTaskRequest(string Title);
}
