using System.Threading.Tasks;
using artstesh.ru.Services;
using Microsoft.AspNetCore.Mvc;

namespace artstesh.ru.Controllers
{
    public class ReadingController : Controller
    {
        private readonly IArticleService _articleService;

        public ReadingController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: Reading
        public async Task<ActionResult> Index()
        {
            var models = await _articleService.Get();
            return View(models);
        }

        // GET: Reading/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _articleService.GetCached(id);
            return View(model);
        }
    }
}