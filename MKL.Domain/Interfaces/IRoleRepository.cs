using Microsoft.AspNetCore.Identity;
using MKL.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRole>> GetAllAsync();
        Task<ApplicationRole> GetByIdAsync(Guid id);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
