using System;
using System.Threading.Tasks;
using artstesh.data.Entities;
using artstesh.data.Models;
using artstesh.data.Repositories;
using artstesh.data.Services;
using artstesh.tests.FakeFactories;
using Moq;
using Xunit;

namespace artstesh.tests.Data.Services
{
    public class FeedbackServiceTests
    {
        private readonly Mock<IFeedbackRepository> _repository;
        private readonly FeedbackService _service;

        public FeedbackServiceTests()
        {
            _repository = new Mock<IFeedbackRepository>(MockBehavior.Strict);
            _service = new FeedbackService(_repository.Object);
        }

        [Theory]
        [AutoMoqData]
        public async Task Add(FeedbackModel model, bool expected)
        {
            _repository.Setup(e => e.Add(It.IsAny<Feedback>())).ReturnsAsync(expected);
            //
            var result = await _service.Add(model);
            //
            Assert.True(result == expected);
            _repository.Verify(t => t.Add(It.Is<Feedback>(e => e.Created.Date == DateTime.Now.Date)));
        }
    }
}