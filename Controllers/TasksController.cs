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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> Update(int id, [FromBody] TaskUpdateDto dto, CancellationToken cancellationToken)
        {
            var updated = await _taskService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _taskService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<TaskResponseDto>>> GetAll(
        [FromQuery] Models.TaskState? status,
        [FromQuery] bool sortDescending = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
        {
            var parameters = new TaskQueryParameters
            {
                Status = status,
                SortDescending = sortDescending,
                PageNumber = pageNumber < 1 ? 1 : pageNumber,
                PageSize = pageSize
            };

            var result = await _taskService.GetAllAsync(parameters, cancellationToken);
            return Ok(result);
        }
    }
}
