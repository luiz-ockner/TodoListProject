using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Commands;
using TodoList.Application.DTOs;
using TodoList.Application.Queries;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            // O controller envia a Query para o MediatR
            var tasks = await _mediator.Send(new GetTasksQuery());

            // O MediatR encaminha para o Handler (GetTasksQueryHandler) e retorna o resultado
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 1. Cria o Command a partir do DTO de requisição
            var command = new CreateTaskCommand(request.Title);

            // 2. Envia o Command
            var newTaskId = await _mediator.Send(command);

            // 3. Retorna 201 Created com a rota para o novo recurso
            return CreatedAtAction(nameof(GetTasks), new { id = newTaskId }, newTaskId);
        }
    }
}
