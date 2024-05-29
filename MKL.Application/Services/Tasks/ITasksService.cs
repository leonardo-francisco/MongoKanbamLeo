using MKL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Tasks
{
    public interface ITasksService
    {
        Task<IEnumerable<TasksDto>> GetAllTasksAsync();
        Task<TasksDto> GetTasksByIdAsync(Guid id);
        Task<IEnumerable<TasksDto>> GetTasksByProjectIdAsync(Guid projectId);
        Task CreateTasksAsync(TasksDto taskDto);
        Task UpdateTasksAsync(TasksDto taskDto);
        Task DeleteTasksAsync(Guid id);
    }
}
