using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.DTO
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [RegularExpression("(To Do|In Progress|Done)", ErrorMessage = "Invalid status. Allowed values are 'To Do', 'In Progress', 'Done'")]
        public string? Status { get; set; }



    }
}

