using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKL.Application.Dto;
using MKL.Application.Services.Tasks;

namespace MKL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _tasksService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var tasks = await _tasksService.GetTasksByIdAsync(id);
            return Ok(tasks);
        }

        [HttpGet("byProject/{id}")]
        public async Task<IActionResult> GetTasksByProjectId(Guid id)
        {
            var tasks = await _tasksService.GetTasksByProjectIdAsync(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TasksDto tasksDto)
        {
            if (tasksDto == null) return BadRequest();
            await _tasksService.CreateTasksAsync(tasksDto);
            return Ok(new
            {
                Message = "Tarefa criada com sucesso",
                Tasks = tasksDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(TasksDto tasksDto)
        {
            if (tasksDto == null) return BadRequest();
            await _tasksService.UpdateTasksAsync(tasksDto);
            return Ok(new
            {
                Message = "Tarefa atualizada com sucesso",
                Tasks = tasksDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {          
            await _tasksService.DeleteTasksAsync(id);
            return NoContent();
        }
    }
}
