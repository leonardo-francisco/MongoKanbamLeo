using MKL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Project
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(Guid id);
        Task CreateProjectAsync(ProjectDto projectDto);
        Task UpdateProjectAsync(ProjectDto projectDto);
        Task DeleteProjectAsync(Guid id);
    }
}
