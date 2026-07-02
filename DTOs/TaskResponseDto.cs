using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskState Status { get; set; } = TaskState.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static TaskResponseDto FromEntity(TaskItem entity) => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
