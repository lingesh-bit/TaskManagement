using TaskManagement.DTOs;

namespace TaskManagement.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResultDto<TaskResponseDto>> GetAllAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default);

        Task<TaskResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<TaskResponseDto> CreateAsync(TaskCreateDto dto, CancellationToken cancellationToken = default);

        Task<TaskResponseDto?> UpdateAsync(int id, TaskUpdateDto dto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
