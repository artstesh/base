using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using artstesh.data.Converters;
using artstesh.data.Entities;
using artstesh.data.Models;
using artstesh.data.Repositories;
using artstesh.data.Services;
using artstesh.tests.FakeFactories;
using Moq;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Services
{
    public class SubscribeServiceTests
    {
        private readonly Mock<ISubscribeRepository> _repository;
        private readonly SubscribeService _service;

        public SubscribeServiceTests()
        {
            _repository = new Mock<ISubscribeRepository>(MockBehavior.Strict);
            _service = new SubscribeService(_repository.Object);
        }

        [Theory]
        [AutoMoqData]
        public async Task Add(SubscribeModel model, int expectedId)
        {
            model.IsActive = false;
            model.Secret = string.Empty;
            _repository.Setup(e => e.Add(It.IsAny<Subscribe>())).ReturnsAsync(expectedId);
            //
            var result = await _service.Add(model);
            //
            Assert.True(result == expectedId);
            _repository.Verify(t => t.Add(It.Is<Subscribe>(e => e.BeginDate.Date == DateTime.Now.Date)));
            _repository.Verify(t => t.Add(It.Is<Subscribe>(e => e.IsActive)));
            _repository.Verify(t => t.Add(It.Is<Subscribe>(e => !string.IsNullOrWhiteSpace(e.Secret))));
        }

        [Theory]
        [AutoMoqData]
        public async Task Get_Success(Subscribe entity)
        {
            var list = new List<Subscribe> {entity};
            _repository.Setup(e => e.Get()).ReturnsAsync(list);
            //
            var result = await _service.Get();
            //
            var source = entity.ToModel().AsSource().OfLikeness<SubscribeModel>();
            Assert.True(result.Count == 1);
            Assert.True(source.Equals(result.First()));
        }

        [Theory]
        [AutoMoqData]
        public async Task Unsubscribe_Success(string secret, bool expected)
        {
            _repository.Setup(e => e.Unsubscribe(secret)).ReturnsAsync(expected);
            //
            var result = await _service.Unsubscribe(secret);
            //
            Assert.True(result == expected);
        }
    }
}