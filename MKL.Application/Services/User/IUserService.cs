using Microsoft.AspNetCore.Identity;
using MKL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Application.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(UserDto userDto);
        Task<IdentityResult> UpdateUserAsync(UserDto userDto);
        Task<IdentityResult> DeleteUserAsync(Guid id);
        Task<SignInResult> VerifyPasswordSignInAsync(UserDto userDto, string password);
    }
}
