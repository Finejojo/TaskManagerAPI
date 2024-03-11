using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetTasks();
        Task<TaskDto> GetTaskById(int id);
        Task<CreateTaskDto> CreateTask(CreateTaskDto taskDto);
        Task<UpdateTaskDto> UpdateTask(int id, UpdateTaskDto taskDto);
        Task<string> DeleteTask(int id);
    }
}
