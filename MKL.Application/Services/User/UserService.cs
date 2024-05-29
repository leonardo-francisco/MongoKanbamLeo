using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MKL.Application.Dto;
using MKL.Domain.Entities;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<MKL.Domain.Entities.User>(userDto);
            return await _userRepository.CreateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
             
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<MKL.Domain.Entities.User>(userDto);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<SignInResult> VerifyPasswordSignInAsync(UserDto userDto, string password)
        {
            var user = _mapper.Map<MKL.Domain.Entities.User>(userDto);
            return await _userRepository.VerifyPasswordSignInAsync(user, password);
        }
    }
}
