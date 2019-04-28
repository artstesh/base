using System.Threading.Tasks;
using artstesh.data.Models;
using artstesh.ru.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace artstesh.ru.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: Article
        public async Task<ActionResult> Index()
        {
            var models = await _articleService.Get();
            return View(models);
        }
        [Authorize]
        // GET: Article/Create
        public ActionResult Create()
        {
            return View(new ArticleModel());
        }
        [Authorize]
        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticleModel model)
        {
            try
            {
                if((await _articleService.Create(model)) > 0)
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //ignored
            }
            return View(model);
        }
        [Authorize]
        // GET: Article/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _articleService.GetCached(id);
            return View(model);
        }
        [Authorize]
        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ArticleModel model)
        {
            try
            {
                if (await _articleService.Update(model))
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                //ignored
            }
            return View(model);
        }
    }
}