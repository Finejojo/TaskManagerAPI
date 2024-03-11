using TaskManagerAPI.Repositories;
using System;
using System.Collections.Generic;
using TaskManagerAPI.Models;
using TaskManagerAPI.DTO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TaskManagerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public IEnumerable<TaskDto> GetTasks()
        {
            try
            {
                _logger.LogInformation("Retrieving all tasks");
                var tasks = _taskRepository.GetTasks();
                var taskDtos = new List<TaskDto>();
                foreach (var task in tasks)
                {
                    taskDtos.Add(new TaskDto
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Description = task.Description,
                        Status = task.Status,
                    });
                }
                _logger.LogInformation($"Successfully retrieved {taskDtos.Count} tasks.");
                return taskDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting tasks: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving task by ID: {id}");
                var task = await _taskRepository.GetTaskById(id);

                if (task == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found.");
                    throw new KeyNotFoundException($"A task with ID {id} was not found.");
                }

                var taskDto = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                };
                _logger.LogInformation($"Successfully retrieved task with ID: {id}");
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting task with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<CreateTaskDto> CreateTask(CreateTaskDto taskDto)
        {
            try
            {
                _logger.LogInformation($"Creating new task with Title: {taskDto.Title}");
                var taskModel = new TaskModel
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Status = taskDto.Status,
                };

                var createdTask = await _taskRepository.CreateTask(taskModel);
                
              

                _logger.LogInformation($"Successfully created task");
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating task: {ex.Message}");
                throw;
            }
        }

        public async Task<UpdateTaskDto> UpdateTask(int id, UpdateTaskDto taskDto)
        {
            try
            {
                _logger.LogInformation($"Updating task with ID: {id}");
                var existingTask = await _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    _logger.LogWarning($"No task found with ID {id} for update.");
                    return null;
                }

                existingTask.Title = taskDto.Title;
                existingTask.Description = taskDto.Description;
                existingTask.Status = taskDto.Status;

                var updatedTask = await _taskRepository.UpdateTask(id, existingTask);

                _logger.LogInformation($"Successfully updated task with ID: {id}");
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating task with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> DeleteTask(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting task with ID: {id}");
                var deletedTaskMessage = await _taskRepository.DeleteTask(id);
                _logger.LogInformation($"Delete operation: {deletedTaskMessage}");
                return deletedTaskMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting task with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
