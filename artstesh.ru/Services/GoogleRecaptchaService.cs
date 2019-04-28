using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using artstesh.ru.Models;
using C2c.Config;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace artstesh.ru.Services
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        private readonly IConfigSettings _settings;
        private readonly HttpClient _httpClient;

        public GoogleRecaptchaService(IConfigSettings settings)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://www.google.com");

            _settings = settings;
        }

        public async Task<RecaptchaResponse> Validate(IFormCollection form)
        {
            var gRecaptchaResponse = form["g-recaptcha-response"];
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _settings.ApplicationKeys.GoogleSecretKey),
                new KeyValuePair<string, string>("response", gRecaptchaResponse)
            });

            var response = await _httpClient.PostAsync("/recaptcha/api/siteverify", content);
            var resultContent = await response.Content.ReadAsStringAsync();
            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(resultContent);

            return captchaResponse;
        }
    }
}