using Microsoft.AspNetCore.Mvc;
using MKL.Web.Models;
using MKL.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MKL.Web.Controllers
{
    public class KanbanController : Controller
    {
        private readonly KanbanService _kanbanService;

        public KanbanController(KanbanService kanbanService)
        {
            _kanbanService = kanbanService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllProjects()
        {
            var projects = await _kanbanService.GetAllProjectsAsync();
            return Json(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _kanbanService.GetTaskByIdAsync(id);
            return Json(task);
        }

        [HttpGet]
        public async Task<JsonResult> GetTasksByProjectsId(Guid projectId)
        {
            var tasks = await _kanbanService.GetTasksByProjectIdAsync(projectId);
            return Json(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }

            var response = await _kanbanService.CreateProjectAsync(model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(result);
                return RedirectToAction("Index", "Kanban");
            }

            ModelState.AddModelError(string.Empty, "Invalid project attempt.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TasksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _kanbanService.CreateTaskAsync(model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var createdTask = JsonConvert.DeserializeObject<TasksViewModel>(result);
                return Ok(createdTask);
            }

            return StatusCode((int)response.StatusCode, "Failed to create task.");
        }

    }
}
