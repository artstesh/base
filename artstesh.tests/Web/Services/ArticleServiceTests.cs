using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.DbContext;
using artstesh.data.Entities;
using artstesh.data.Helpers;
using artstesh.data.Models;
using artstesh.data.Repositories;
using artstesh.ru.Services;
using artstesh.tests.FakeFactories;
using C2c.Helper;
using C2c.Services.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace artstesh.tests.Web.Services
{
    public class ArticleServiceTests
    {
        private readonly Mock<IArticleRepository> _repository;
        private readonly Mock<ICacheHelper> _cache;
        private readonly ArticleService _service;

        public ArticleServiceTests()
        {
            _cache = new Mock<ICacheHelper>(MockBehavior.Strict);
            _repository = new Mock<IArticleRepository>(MockBehavior.Strict);
            _service = new ArticleService(_repository.Object, _cache.Object);
        }
        
        [Theory, AutoMoqData]
        public async Task Get(Article article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            var list = new List<Article> {article};
            _repository.Setup(e => e.Get()).ReturnsAsync(list);
            //
            var result = await _service.Get();
            //
            var expected = result.First(e => e.Preview == article.Preview);
            Assert.True(expected.Equals(article.ToModel()));
        }

        [Theory, AutoMoqData]
        public async Task Create(ArticleModel article, int expected)
        {
            _repository.Setup(e => e.Create(It.IsAny<Article>())).ReturnsAsync(expected);
            //
            var result = await _service.Create(article);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Create(It.IsAny<Article>()), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Update(ArticleModel model, bool expected)
        {
            _repository.Setup(e => e.Update(It.IsAny<Article>())).ReturnsAsync(expected);
            //
            var result = await _service.Update(model);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Update(It.IsAny<Article>()), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task Delete(int id, bool expected)
        {
            _repository.Setup(e => e.Delete(id)).ReturnsAsync(expected);
            //
            var result = await _service.Delete(id);
            //
            Assert.True(result == expected);
            _repository.Verify(e => e.Delete(id), Times.Once);
        }
        [Theory]
        [AutoMoqData]
        public async Task GetSdItems_With_Cache(ArticleModel article)
        {
            var bytes = ObjectByteConverter.ObjectToByteArray(article);
            _cache.Setup(e => e.Get("article_"+article.Id)).ReturnsAsync(bytes);
            //
            var result = await _service.GetCached(article.Id);
            //
            Assert.True(result.Equals(article));
        }

        [Theory]
        [AutoMoqData]
        public async Task GetResourseLimitsCached_Without_Cache_Success(Article article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            //
            _cache.Setup(e => e.Get("article_"+article.Id)).ReturnsAsync((byte[]) null);
            _repository.Setup(e => e.Get(article.Id)).ReturnsAsync(article);
            _cache.Setup(e => e.Set("article_"+article.Id, It.IsAny<ArticleModel>(), -1)).Returns(Task.Run(() => { }));
            //
            var result = await _service.GetCached(article.Id);
            //
            Assert.True(result.Equals(article.ToModel()));
        }
    }
}