using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "DueDate is required.")]
        [NotInPast(ErrorMessage = "DueDate cannot be in the past.")]
        public DateTime DueDate { get; set; }
    }
}

public class NotInPastAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            var utcValue = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            if (utcValue < DateTime.UtcNow)
            {
                return new ValidationResult(ErrorMessage ?? "Date cannot be in the past.");
            }
        }

        return ValidationResult.Success;
    }
}
