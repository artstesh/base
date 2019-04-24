using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Models;
using artstesh.data.Repositories;
using C2c.Helper;
using C2c.Services.Converters;

namespace artstesh.ru.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly ICacheHelper _cache;
        private static string _cacheKey;

        public ArticleService(IArticleRepository repository, ICacheHelper cache)
        {
            _repository = repository;
            _cache = cache;
        }

        static ArticleService()
        {
            _cacheKey = "article_";
        }

        public async Task<List<ArticleModel>> Get()
        {
            var articles = await _repository.Get();
            return articles.Select(e => e.ToModel()).ToList();
        }

        public async Task<ArticleModel> GetCached(int id)
        {
            var bytes = await _cache.Get(_cacheKey+id);
            var article = (ArticleModel) null;
            if (bytes != null && bytes.Length > 0)
                article = ObjectByteConverter.ByteArrayToObject<ArticleModel>(bytes);
            if (article != null) return article;
            article = (await _repository.Get(id)).ToModel();
            await _cache.Set(_cacheKey+id, article);
            return article;
        }

        public async Task<int> Create(ArticleModel article)
        {
            article.Created = DateTime.Now;
            return await _repository.Create(article.FromModel());
        }

        public async Task<bool> Update(ArticleModel article)
        {
            return await _repository.Update(article.FromModel());
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}