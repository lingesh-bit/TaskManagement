using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Interfaces;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> Create([FromBody] TaskCreateDto dto, CancellationToken cancellationToken)
        {
            var created = await _taskService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var task = await _taskService.GetByIdAsync(id, cancellationToken);
            return Ok(task);
        }
    }
}
