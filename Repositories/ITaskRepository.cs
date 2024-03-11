using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetTasks();
        Task<TaskModel?> GetTaskById( int id);
        Task<TaskModel?> CreateTask(TaskModel task);
        Task<TaskModel?> UpdateTask(int id, TaskModel task);
        Task<string> DeleteTask(int id);
    }
}
