using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Entities;
using artstesh.data.Helpers;
using artstesh.data.Models;
using artstesh.data.Repositories;
using artstesh.data.Services;
using artstesh.tests.FakeFactories;
using Moq;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Services
{
    public class ArticleServiceTests
    {
        private readonly Mock<IArticleRepository> _repository;
        private readonly ArticleService _service;

        public ArticleServiceTests()
        {
            _repository = new Mock<IArticleRepository>(MockBehavior.Strict);
            _service = new ArticleService(_repository.Object);
        }

        [Theory]
        [AutoMoqData]
        public async Task Get_Success(Article article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            var list = new List<Article> {article};
            _repository.Setup(e => e.Get()).ReturnsAsync(list);
            //
            var result = await _service.Get();
            //
            var source = article.ToModel().AsSource().OfLikeness<ArticleModel>();
            Assert.True(result.Count == 1);
            Assert.True(source.Equals(result.First()));
        }

        [Theory]
        [AutoMoqData]
        public async Task Get_By_Id_Success(Article article)
        {
            article.Text = StringCompressor.CompressString(article.Text);
            _repository.Setup(e => e.Get(article.Id)).ReturnsAsync(article);
            //
            var result = await _service.Get(article.Id);
            //
            var source = article.ToModel().AsSource().OfLikeness<ArticleModel>();
            Assert.True(source.Equals(result));
        }

        [Theory]
        [AutoMoqData]
        public async Task Delete_Success(int id, bool expected)
        {
            _repository.Setup(e => e.Delete(id)).ReturnsAsync(expected);
            //
            var result = await _service.Delete(id);
            //
            Assert.True(result == expected);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Success(ArticleModel model, bool expected)
        {
            _repository.Setup(e => e.Update(It.IsAny<Article>())).ReturnsAsync(expected);
            //
            var result = await _service.Update(model);
            //
            Assert.True(result == expected);
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_Success(ArticleModel model, int expectedId)
        {
            _repository.Setup(e => e.Create(It.IsAny<Article>())).ReturnsAsync(expectedId);
            //
            var result = await _service.Create(model);
            //
            Assert.True(result == expectedId);
            _repository.Verify(t => t.Create(It.Is<Article>(e => e.Created.Date == DateTime.Now.Date)));
        }
    }
}