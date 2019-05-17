using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.DbContext;
using artstesh.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace artstesh.data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;

        public ArticleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> Get(bool unpublished = false)
        {
            return _context.Articles
                .Where(e => unpublished || e.Published)
                .OrderByDescending(e => e.Created).ToList();
        }

        public async Task<int> Create(Article article)
        {
            await _context.AddAsync(article);
            await _context.SaveChangesAsync();
            return article.Id;
        }

        public async Task<bool> Update(Article article)
        {
            var entity = await _context.Articles.FirstOrDefaultAsync(e => e.Id == article.Id);
            if (entity == null) return false;
            entity.Text = article.Text;
            entity.Preview = article.Preview;
            entity.Title = article.Title;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Articles.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return false;
            _context.Articles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Article> Get(int id)
        {
            var entity = await _context.Articles.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task Publish(int articleId)
        {
            var entity = await _context.Articles.FirstOrDefaultAsync(e => e.Id == articleId);
            if (entity == null) return;
            entity.Published = true;
            await _context.SaveChangesAsync();
        }
    }
}