using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Models;
using artstesh.data.Repositories;

namespace artstesh.data.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ArticleModel>> Get()
        {
            
            var articles = await _repository.Get();
            return articles.Select(e => e.ToModel()).ToList();
        }

        public async Task<int> Create(ArticleModel article)
        {
            article.Created = DateTime.Now;
            var entity = article.FromModel();
            return await _repository.Create(entity);
        }

        public async Task<bool> Update(ArticleModel article)
        {
            var entity = article.FromModel();
            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ArticleModel> Get(int id)
        {
            var entity = await _repository.Get(id);
            return entity.ToModel();
        }
    }
}