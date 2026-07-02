using TaskManagement.Models;

namespace TaskManagement.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetAllAsync(
            TaskState? status,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default);

        Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);

        Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default);

        Task<List<TaskItem>> GetOverduePendingTasksAsync(DateTime asOfUtc, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
