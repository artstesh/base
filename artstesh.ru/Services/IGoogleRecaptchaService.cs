using System.Threading.Tasks;
using artstesh.ru.Models;
using Microsoft.AspNetCore.Http;

namespace artstesh.ru.Services
{
    public interface IGoogleRecaptchaService
    {
        Task<RecaptchaResponse> Validate(IFormCollection form);
    }
}