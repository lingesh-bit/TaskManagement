using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskUpdateDto
    {

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "DueDate is required.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(TaskState), ErrorMessage = "Status must be one of: Pending, Completed, Expired.")]
        public TaskState Status { get; set; }
    }
}
