using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using artstesh.ru.Services;
using C2c.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QuizWeb.Models;

namespace QuizWeb.Controllers.WebControllers
{
    public class AccountController : Controller
    {
        private readonly IGoogleRecaptchaService _googleRecaptchaService;
        private readonly IConfigSettings _settings;

        public AccountController(IConfigSettings settings, IGoogleRecaptchaService googleRecaptchaService)
        {
            _settings = settings;
            _googleRecaptchaService = googleRecaptchaService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var captchaResponse = await _googleRecaptchaService.Validate(Request.Form);
            if (!captchaResponse.Success)
            {
                ModelState.AddModelError("reCaptchaError",
                    "Есть мнение, что Вы робот... повторим?");
                return View(model);
            }

            if (!_settings.CheckPassword(model.Login, model.Password))
                return View(new LoginModel());
            await Authenticate(model.Login);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
            await HttpContext.AuthenticateAsync();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}