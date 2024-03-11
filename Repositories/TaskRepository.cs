using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using System;
using System.Collections.Generic;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(AppDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<TaskModel> GetTasks()
        {
            try
            {
                _logger.LogInformation("Retrieving all tasks");
                var tasks = _context.Tasks.ToList();
                _logger.LogInformation($"Retrieved {tasks.Count} tasks successfully.");
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving tasks: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskModel?> GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving task by ID: {id}");
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found.");
                }
                else
                {
                    _logger.LogInformation($"Successfully retrieved task with ID: {id}");
                }
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving task with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<TaskModel?> CreateTask(TaskModel task)
        {
            try
            {
                _logger.LogInformation($"Creating new task with Title: {task.Title}");
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully created task with ID: {task.Id}");
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating a task: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskModel?> UpdateTask(int id, TaskModel task)
        {
            try
            {
                _logger.LogInformation($"Updating task with ID: {id}");
                var existingTask = await GetTaskById(id);
                if (existingTask == null)
                {
                    _logger.LogWarning($"No task found with ID {id} for update.");
                    return null;
                }
                _context.Entry(existingTask).CurrentValues.SetValues(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully updated task with ID: {task.Id}");
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating the task with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<string> DeleteTask(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting task with ID: {id}");

                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found for deletion.");
                    return "Task not found";
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully deleted task with ID: {id}");
                return "Task has been deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the task with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
