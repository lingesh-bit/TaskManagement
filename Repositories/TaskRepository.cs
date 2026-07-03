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
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetAllAsync(TaskState? status, bool sortDescending, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public Task<List<TaskItem>> GetOverduePendingTasksAsync(DateTime asOfUtc, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
