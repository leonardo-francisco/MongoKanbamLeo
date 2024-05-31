using Microsoft.AspNetCore.Mvc;
using MKL.Web.Models;
using MKL.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MKL.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _authService.LoginAsync(model);

            if (response != null)
            {
                var result = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(result);
                string token = json["token"].ToString();

                // Salvar o token em um cookie ou sessão
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Usuário ou senha inválida";
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _authService.RegisterAsync(model);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Erro ao cadastrar";
            ModelState.AddModelError(string.Empty, "Registration failed.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
