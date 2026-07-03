using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.BackgroundJobs
{
    public class ExpiredTaskBackgroundService : BackgroundService
    {

        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(5);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredTaskBackgroundService> _logger;

        public ExpiredTaskBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ExpiredTaskBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ExpiredTaskBackgroundService started. Interval: {Interval}", Interval);

            using var timer = new PeriodicTimer(Interval);

            await ExpireOverdueTasksAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
                await ExpireOverdueTasksAsync(stoppingToken);
            }
        }

        private async Task ExpireOverdueTasksAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();

                var now = DateTime.UtcNow;
                var overdueTasks = await repository.GetOverduePendingTasksAsync(now, cancellationToken);

                if (overdueTasks.Count == 0)
                {
                    _logger.LogDebug("No overdue pending tasks found at {Timestamp}", now);
                    return;
                }

                foreach (var task in overdueTasks)
                {
                    task.Status = TaskState.Expired;
                    task.UpdatedAt = now;
                }

                var affected = await repository.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Expired {Count} overdue task(s) at {Timestamp}", affected, now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while expiring overdue tasks.");
            }
        }
    }
}
