using MKL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<Tasks> GetByIdAsync(Guid id);
        Task<IEnumerable<Tasks>> GetByProjectIdAsync(Guid projectId);
        Task CreateAsync(Tasks task);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(Guid id);
    }
}
