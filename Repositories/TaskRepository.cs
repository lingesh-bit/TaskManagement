using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }

        public Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }


        public async Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetAllAsync(
        TaskState? status,
        bool sortDescending,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        {
            var query = _context.Tasks.AsNoTracking().AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = sortDescending
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<List<TaskItem>> GetOverduePendingTasksAsync(DateTime asOfUtc, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .Where(t => t.Status == TaskState.Pending && t.DueDate < asOfUtc)
                .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Update(task);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
