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
    public class FeedbackRepositoryTests
    {
        private readonly DataContext _context;
        private FeedbackRepository _repository;

        public FeedbackRepositoryTests()
        {
            _context = ContextFactory.GetContext();
            _repository = new FeedbackRepository(_context);
        }

        [Theory]
        [AutoMoqData]
        public async Task Add_Success(Feedback feedback)
        {
            var result = await _repository.Add(feedback);
            //
            var created = _context.Feedbacks.First(e => e.Email == feedback.Email)
                .AsSource().OfLikeness<Feedback>();
            Assert.True(created.Equals(feedback));
            Assert.True(result);
        }

        [Theory]
        [AutoMoqData]
        public async Task Add_Fail(Feedback feedback)
        {
            _repository = new FeedbackRepository(null);
            //
            var result = await _repository.Add(feedback);
            //
            Assert.False(result);
        }
    }
}