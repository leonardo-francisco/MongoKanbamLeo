using AutoMapper;
using MKL.Application.Dto;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Tasks
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public TasksService(ITasksRepository tasksRepository, IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        public async Task CreateTasksAsync(TasksDto taskDto)
        {
            var task = _mapper.Map<MKL.Domain.Entities.Tasks>(taskDto);
            await _tasksRepository.CreateAsync(task);
        }

        public async Task DeleteTasksAsync(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            await _tasksRepository.DeleteAsync(task.Id);
        }

        public async Task<IEnumerable<TasksDto>> GetAllTasksAsync()
        {
            var tasks = await _tasksRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TasksDto>>(tasks);
        }

        public async Task<TasksDto> GetTasksByIdAsync(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            return _mapper.Map<TasksDto>(task);
        }

        public async Task<IEnumerable<TasksDto>> GetTasksByProjectIdAsync(Guid projectId)
        {
            var task = await _tasksRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<TasksDto>>(task);
        }

        public async Task UpdateTasksAsync(TasksDto taskDto)
        {
            var task = _mapper.Map<MKL.Domain.Entities.Tasks>(taskDto);
            await _tasksRepository.UpdateAsync(task);
        }
    }
}
