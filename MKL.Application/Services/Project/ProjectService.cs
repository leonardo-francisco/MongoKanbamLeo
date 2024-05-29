using AutoMapper;
using MKL.Application.Dto;
using MKL.Domain.Entities;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task CreateProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<MKL.Domain.Entities.Project>(projectDto);
            await _projectRepository.CreateAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            await _projectRepository.DeleteAsync(project.Id);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task UpdateProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<MKL.Domain.Entities.Project>(projectDto);
            await _projectRepository.UpdateAsync(project);
        }
    }
}
