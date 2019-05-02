using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Models;
using artstesh.data.Repositories;
using artstesh.data.Services;
using C2c.Helper;
using C2c.Services.Converters;

namespace artstesh.ru.Services
{
    public class ArticleCacheCacheService : IArticleCacheService
    {
        private readonly IArticleService _repository;
        private readonly ICacheHelper _cache;
        private static string _cacheKey;

        public ArticleCacheCacheService(IArticleService repository, ICacheHelper cache)
        {
            _repository = repository;
            _cache = cache;
        }

        static ArticleCacheCacheService()
        {
            _cacheKey = "article_";
        }

        public async Task<List<ArticleModel>> Get()
        {
            return await _repository.Get();
        }

        public async Task<ArticleModel> GetCached(int id)
        {
            var bytes = await _cache.Get(_cacheKey + id);
            var article = (ArticleModel) null;
            if (bytes != null && bytes.Length > 0)
                article = ObjectByteConverter.ByteArrayToObject<ArticleModel>(bytes);
            if (article != null) return article;
            article = await _repository.Get(id);
            await _cache.Set(_cacheKey + id, article);
            return article;
        }

        public async Task<int> Create(ArticleModel article)
        {
            return await _repository.Create(article);
        }

        public async Task<bool> Update(ArticleModel article)
        {
            await _cache.Remove(_cacheKey + article.Id);
            return await _repository.Update(article);
        }

        public async Task<bool> Delete(int id)
        {
            await _cache.Remove(_cacheKey + id);
            return await _repository.Delete(id);
        }
    }
}