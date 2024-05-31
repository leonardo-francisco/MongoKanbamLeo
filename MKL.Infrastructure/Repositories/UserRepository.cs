using Microsoft.AspNetCore.Identity;
using MKL.Domain.Entities;
using MKL.Domain.Identity;
using MKL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKL.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.EmailUser,
                PasswordHash = user.UserPassword
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, user.UserPassword);
            if (result.Succeeded && user.Name == "Administrador")
            {
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "User");
            }
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(Convert.ToString(id));
            await _userManager.DeleteAsync(appUser);
            return IdentityResult.Success;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var appUser = _userManager.Users.ToList();
            var users = new List<User>();
            foreach (var item in appUser)
            {
                var roles = await _userManager.GetRolesAsync(item);
                var roleName = roles.FirstOrDefault();

                users.Add(new User
                {
                    Id = item.Id,
                    Name = item.UserName,
                    EmailUser = item.Email,
                    Role = roleName
                });
            }
            
            return users;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null) return null;

            var roles = await _userManager.GetRolesAsync(appUser);
            var roleName = roles.FirstOrDefault();

            var user = new User
            {
                Id = appUser.Id,
                Name = appUser.UserName,
                EmailUser = appUser.Email,
                UserPassword = appUser.PasswordHash,
                Role = roleName
            };
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(Convert.ToString(id));
            var roles = await _userManager.GetRolesAsync(appUser);
            var roleName = roles.FirstOrDefault();

            var user = new User
            {
                Id = appUser.Id,
                Name = appUser.UserName,
                EmailUser = appUser.Email,
                Role = roleName
            };
            return user;
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            var appUser = await _userManager.FindByIdAsync(Convert.ToString(user.Id));
            await _userManager.UpdateAsync(appUser);
            return IdentityResult.Success;
        }

        public async Task<SignInResult> VerifyPasswordSignInAsync(User user, string password)
        {
            var signedUser = await _userManager.FindByEmailAsync(user.EmailUser);
            return await _signInManager.PasswordSignInAsync(signedUser, password, false, false);
        }
    }
}
