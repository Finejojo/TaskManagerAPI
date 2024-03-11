using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string?  Status { get; set; }// "To Do," "In Progress," "Done"



        public TaskModel(CreateTaskDto task)
        {
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
        }

        public TaskModel(UpdateTaskDto task)
        {
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
        }
        public TaskModel()
        {

        }
    }

       

}
        
    


