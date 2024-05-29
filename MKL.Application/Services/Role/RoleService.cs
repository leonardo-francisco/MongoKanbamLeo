using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MKL.Application.Dto;
using MKL.Domain.Identity;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);
            return await _roleRepository.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(Guid id)
        {
            return await _roleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);
            return await _roleRepository.UpdateAsync(role);
        }
    }
}
