using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKL.Application.Dto;
using MKL.Application.Services.Project;

namespace MKL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDto projectDto)
        {
            if (projectDto == null) return BadRequest();
            await _projectService.CreateProjectAsync(projectDto);
            return Ok(new
            {
                Message = "Projeto criado com sucesso",
                Project = projectDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(ProjectDto projectDto)
        {
            if (projectDto == null) return BadRequest();
            await _projectService.UpdateProjectAsync(projectDto);
            return Ok(new
            {
                Message = "Projeto atualizado com sucesso",
                Project = projectDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {            
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
