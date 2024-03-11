using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public TaskDto(TaskModel task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
        }
        public TaskDto()
        {
            
        }

    }
}
