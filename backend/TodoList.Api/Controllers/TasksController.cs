using Microsoft.AspNetCore.Mvc;
using MediatR;
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
    }
}
