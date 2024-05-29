using Microsoft.AspNetCore.Identity;
using MKL.Application.Dto;
using MKL.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Role
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(Guid id);
        Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
        Task<IdentityResult> UpdateRoleAsync(RoleDto roleDto);
        Task<IdentityResult> DeleteRoleAsync(Guid id);
    }
}
