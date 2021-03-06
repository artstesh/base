using System.Threading.Tasks;
using artstesh.core.Config;
using artstesh.data.Models;
using artstesh.data.Services;
using artstesh.ru.Models;
using artstesh.ru.Services;
using Microsoft.AspNetCore.Mvc;

namespace artstesh.ru.Controllers
{
    public class SubController : Controller
    {
        private readonly IGoogleRecaptchaService _googleRecaptchaService;
        private readonly ISubscribeService _service;
        private readonly IConfigSettings _settings;

        public SubController(IConfigSettings settings, IGoogleRecaptchaService googleRecaptchaService,
            ISubscribeService service)
        {
            _settings = settings;
            _googleRecaptchaService = googleRecaptchaService;
            _service = service;
        }

        [HttpGet]
        public IActionResult Sub()
        {
            var model = new SubViewModel();
            model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
            model.SubscribeModel = new SubscribeModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sub(SubViewModel model)
        {
            var captchaResponse = await _googleRecaptchaService.Validate(Request.Form);
            if (!captchaResponse.Success)
            {
                ModelState.AddModelError("reCaptchaError",
                    "Есть мнение, что Вы робот... повторим?");
                model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
                return View(model);
            }

            await _service.Add(model.SubscribeModel);
            return RedirectToAction("Success");
        }

        [HttpGet]
        public async Task<IActionResult> UnSub(string secret)
        {
            await _service.Unsubscribe(secret);
            return View();
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}