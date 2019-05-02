using System.Threading.Tasks;
using artstesh.data.Models;
using artstesh.data.Services;
using artstesh.ru.Models;
using artstesh.ru.Services;
using C2c.Config;
using Microsoft.AspNetCore.Mvc;

namespace artstesh.ru.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IGoogleRecaptchaService _googleRecaptchaService;
        private readonly IFeedbackService _service;
        private readonly IConfigSettings _settings;

        public FeedbackController(IConfigSettings settings, IGoogleRecaptchaService googleRecaptchaService,
            IFeedbackService service)
        {
            _settings = settings;
            _googleRecaptchaService = googleRecaptchaService;
            _service = service;
        }

        [HttpGet]
        public IActionResult Write()
        {
            var model = new FeedbackViewModel();
            model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
            model.FeedbackModel = new FeedbackModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Write(FeedbackViewModel model)
        {
            var captchaResponse = await _googleRecaptchaService.Validate(Request.Form);
            if (!captchaResponse.Success)
            {
                ModelState.AddModelError("reCaptchaError",
                    "Есть мнение, что Вы робот... повторим?");
                model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
                return View(model);
            }

            var result = await _service.Add(model.FeedbackModel);
            if (!result)
            {
                ModelState.AddModelError("reCaptchaError",
                    "Мне стыдно в этом признаться, но кажется что-то сломалось :( Может быть попробовать еще раз?");
                model.GoogleKey = _settings.ApplicationKeys.GoogleKey;
                return View(model);
            }
            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}