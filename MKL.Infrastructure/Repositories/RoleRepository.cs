using Microsoft.AspNetCore.Identity;
using MKL.Domain.Entities;
using MKL.Domain.Identity;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            ApplicationRole appRole = new ApplicationRole
            {
                Name = role.Name
               
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var appRole = await _roleManager.FindByIdAsync(Convert.ToString(id));
            await _roleManager.DeleteAsync(appRole);
            return IdentityResult.Success;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllAsync()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }

        public async Task<ApplicationRole> GetByIdAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(Convert.ToString(id));
            return role;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            var appRole = await _roleManager.FindByIdAsync(Convert.ToString(role.Id));
            await _roleManager.UpdateAsync(appRole);
            return IdentityResult.Success;
        }
    }
}
