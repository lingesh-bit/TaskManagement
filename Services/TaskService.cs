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

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            await _repository.DeleteAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted task {TaskId}", id);

            return true;
        }

        public async Task<PagedResultDto<TaskResponseDto>> GetAllAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _repository.GetAllAsync(
                parameters.Status,
                parameters.SortDescending,
                parameters.PageNumber,
                parameters.PageSize,
                cancellationToken);

            return new PagedResultDto<TaskResponseDto>
            {
                Items = items.Select(TaskResponseDto.FromEntity),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<TaskResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            return task is null ? null : TaskResponseDto.FromEntity(task);
        }

        public async Task<TaskResponseDto?> UpdateAsync(int id, TaskUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
            {
                return null;
            }

            entity.Title = dto.Title.Trim();
            entity.Description = dto.Description;
            entity.DueDate = dto.DueDate;
            entity.Status = dto.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated task {TaskId}", id);

            return TaskResponseDto.FromEntity(entity);
        }
    }
}
