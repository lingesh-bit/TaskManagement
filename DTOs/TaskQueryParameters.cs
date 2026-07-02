using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskQueryParameters
    {
        public TaskState? Status { get; set; }

        public bool SortDescending { get; set; } = false;

        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value switch
            {
                <= 0 => 10,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }
    }
}
