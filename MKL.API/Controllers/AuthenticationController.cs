using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKL.API.Configuration;
using MKL.Application.Dto;
using MKL.Application.InputModels;
using MKL.Application.Services.User;

namespace MKL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel login)
        {
            var user = await _userService.GetUserByEmailAsync(login.EmailUser);

            if (user != null)
            {
                var result = await _userService.VerifyPasswordSignInAsync(user, login.UserPassword);
                if (result.Succeeded)
                {
                    var token = TokenConfiguration.GenerateToken(user.Name);
                    user.UserPassword = "";
                    return Ok(new
                    {
                        user = user,
                        token = token
                    });
                }
                else 
                {
                    return BadRequest("Login falhou: E-mail ou Senha inválida");
                }
            }
            return BadRequest("Usuário não registrado");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var result = await _userService.CreateUserAsync(userDto);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = "Usuário registrado com sucesso"
                });
            }
            return BadRequest(result.Errors);
        }
    }
}
