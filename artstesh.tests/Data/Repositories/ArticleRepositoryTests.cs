using System.Linq;
using System.Threading.Tasks;
using artstesh.data.DbContext;
using artstesh.data.Entities;
using artstesh.data.Repositories;
using artstesh.tests.FakeFactories;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Repositories
{
    public class ArticleRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ArticleRepository _repository;

        public ArticleRepositoryTests()
        {
            _context = ContextFactory.GetContext();
            _repository = new ArticleRepository(_context);
        }
        
        [Theory, AutoMoqData]
        public async Task Get(Article article)
        {
            _context.Add(article);
            _context.SaveChanges();
            //
            var result = await _repository.Get();
            //
            var expected = result.First(e => e.Text == article.Text);
            Assert.True(expected.AsSource().OfLikeness<Article>().Equals(article));
        }

        [Theory, AutoMoqData]
        public async Task Create(Article article)
        {
            var result = await _repository.Create(article);
            //
            var expected = _context.Articles.First(e => e.Text == article.Text);
            Assert.True(result == expected.Id);
            Assert.True(expected.AsSource().OfLikeness<Article>().Equals(article));
        }

        [Theory, AutoMoqData]
        public async Task Update(Article article, Article updated)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
            updated.Id = article.Id;
            //
            await _repository.Update(updated);
            //
            var expected = _context.Articles.First(e => e.Id == article.Id);
            var source = expected.AsSource().OfLikeness<Article>().Without(e => e.Created);
            Assert.True(source.Equals(updated));
        }

        [Theory, AutoMoqData]
        public async Task Update_No_Such_Id(Article article)
        {
            var result = await _repository.Update(article);
            //
            var expected = _context.Articles.FirstOrDefault(e => e.Id == article.Id);
            Assert.False(result);
            Assert.Null(expected);
        }

        [Theory, AutoMoqData]
        public async Task Delete(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
            //
            await _repository.Delete(article.Id);
            //
            var expectedNull = _context.Articles.FirstOrDefault(e => e.Id == article.Id);
            Assert.Null(expectedNull);
        }

        [Theory, AutoMoqData]
        public async Task Delete_No_Such_Id(int id)
        {
            var result = await _repository.Delete(id);
            //
            Assert.False(result);
        }
    }
}