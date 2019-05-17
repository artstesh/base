using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Helpers;
using artstesh.data.Models;
using artstesh.data.Services;
using artstesh.ru.Converters;
using artstesh.ru.Helpers;
using artstesh.ru.Services;
using artstesh.tests.FakeFactories;
using Moq;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Web.Services
{
    public class ArticleServiceTests
    {
        private readonly Mock<ICacheHelper> _cache;
        private readonly ArticleCacheCacheService _cacheCacheService;
        private readonly Mock<IArticleService> _repository;

        public ArticleServiceTests()
        {
            _cache = new Mock<ICacheHelper>(MockBehavior.Strict);
            _repository = new Mock<IArticleService>(MockBehavior.Strict);
            _cacheCacheService = new ArticleCacheCacheService(_repository.Object, _cache.Object);
        }

        [Theory]
        [AutoMoqData]
        public async Task Get(ArticleModel article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            var list = new List<ArticleModel> {article};
            _repository.Setup(e => e.Get()).ReturnsAsync(list);
            //
            var result = await _cacheCacheService.Get();
            //
            var expected = result.First(e => e.Preview == article.Preview);
            var source = article.AsSource().OfLikeness<ArticleModel>();
            Assert.True(source.Equals(expected));
        }

        [Theory]
        [AutoMoqData]
        public async Task Create(ArticleModel article, int expected)
        {
            _repository.Setup(e => e.Create(It.IsAny<ArticleModel>())).ReturnsAsync(expected);
            //
            var result = await _cacheCacheService.Create(article);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Create(It.IsAny<ArticleModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update(ArticleModel model, bool expected)
        {
            _repository.Setup(e => e.Update(It.IsAny<ArticleModel>())).ReturnsAsync(expected);
            _cache.Setup(e => e.Remove("article_" + model.Id)).Returns(Task.Run(() => { }));
            //
            var result = await _cacheCacheService.Update(model);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Update(It.IsAny<ArticleModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Delete(int id, bool expected)
        {
            _repository.Setup(e => e.Delete(id)).ReturnsAsync(expected);
            _cache.Setup(e => e.Remove("article_" + id)).Returns(Task.Run(() => { }));
            //
            var result = await _cacheCacheService.Delete(id);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Delete(id), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetSdItems_With_Cache(ArticleModel article)
        {
            var bytes = ObjectByteConverter.ObjectToByteArray(article);
            _cache.Setup(e => e.Get("article_" + article.Id)).ReturnsAsync(bytes);
            //
            var result = await _cacheCacheService.GetCached(article.Id);
            //
            var source = article.AsSource().OfLikeness<ArticleModel>();
            Assert.True(source.Equals(result));
        }

        [Theory]
        [AutoMoqData]
        public async Task GetResourseLimitsCached_Without_Cache_Success(ArticleModel article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            //
            _cache.Setup(e => e.Get("article_" + article.Id)).ReturnsAsync((byte[]) null);
            _repository.Setup(e => e.Get(article.Id)).ReturnsAsync(article);
            _cache.Setup(e => e.Set("article_" + article.Id, It.IsAny<ArticleModel>(), -1))
                .Returns(Task.Run(() => { }));
            //
            var result = await _cacheCacheService.GetCached(article.Id);
            //
            var source = article.AsSource().OfLikeness<ArticleModel>();
            Assert.True(source.Equals(result));
        }
    }
}