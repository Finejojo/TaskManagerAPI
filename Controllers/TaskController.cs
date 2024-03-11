using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using System.Net.Http;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly AppDbContext _context;
        //private readonly IHttpClientFactory _httpClientFactory;

        public TaskController(ITaskService taskService, AppDbContext context)
        {
            _taskService = taskService;
            _context = context;
           // _httpClientFactory = httpClientFactory;
        }

        // GET: api/tasks
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        // GET: api/tasks/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { status = "failure", code = 99 });
            }
            return Ok(new { status = "success", code = 00, task });
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
        {
            try
            {
                var task = await _taskService.CreateTask(taskDto);
                if (task != null)
                {
                    return Ok(new { status = "success", code = 00, task });
                }
                else
                {
                    // Handle failure (e.g., validation error)
                    return BadRequest(new { status = "failure", code = 99 });
                }
            }
            catch (Exception ex)
            {
                // Log exception details here
                return StatusCode(500, new { status = "failure", code = 99, message = ex.Message });
            }
        }

        // PUT: api/tasks/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto task)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTask(id, task);

                return Ok(new { status = "success", code = 00, updatedTask });
            }
            catch (NotFoundException)
            {
                return NotFound(new { status = "failure", code = 99 });
            }
        }

        // DELETE: api/tasks/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var deletedTask = await _taskService.DeleteTask(id);
                return Ok(new { status = "success", code = 00, deletedTask });
            }
            catch (NotFoundException)
            {
                return NotFound(new { status = "failure", code = 99 });
            }
        }
    }

}

