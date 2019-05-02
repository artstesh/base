using System.Threading.Tasks;
using artstesh.ru.Services;
using Microsoft.AspNetCore.Mvc;

namespace artstesh.ru.Controllers
{
    public class ReadingController : Controller
    {
        private readonly IArticleCacheService _articleCacheService;

        public ReadingController(IArticleCacheService articleCacheService)
        {
            _articleCacheService = articleCacheService;
        }

        // GET: Reading
        public async Task<ActionResult> Index()
        {
            var models = await _articleCacheService.Get();
            return View(models);
        }

        // GET: Reading/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _articleCacheService.GetCached(id);
            return View(model);
        }
    }
}