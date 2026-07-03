using TaskManagement.DTOs;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository repository, ILogger<TaskService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TaskResponseDto> CreateAsync(TaskCreateDto dto, CancellationToken cancellationToken = default)
        {

            var now = DateTime.UtcNow;
            var entity = new TaskItem
            {
                Title = dto.Title.Trim(),
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = TaskState.Pending,
                CreatedAt = now,
                UpdatedAt = now
            };

            var created = await _repository.AddAsync(entity, cancellationToken);
            _logger.LogInformation("Created task {TaskId} - '{Title}' due {DueDate}", created.Id, created.Title, created.DueDate);

            return TaskResponseDto.FromEntity(created);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<TaskResponseDto>> GetAllAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            return task is null ? null : TaskResponseDto.FromEntity(task);
        }

        public Task<TaskResponseDto?> UpdateAsync(int id, TaskUpdateDto dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
