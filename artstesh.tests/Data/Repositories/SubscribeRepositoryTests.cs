using System.Linq;
using System.Threading.Tasks;
using artstesh.data.DbContext;
using artstesh.data.Entities;
using artstesh.data.Repositories;
using artstesh.tests.FakeFactories;
using Microsoft.EntityFrameworkCore;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Repositories
{
    public class SubscribeRepositoryTests
    {
        private readonly DataContext _context;
        private readonly SubscribeRepository _repository;

        public SubscribeRepositoryTests()
        {
            _context = ContextFactory.GetContext();
            _repository = new SubscribeRepository(_context);
        }

        [Theory, AutoMoqData]
        public async Task Add_Success(Subscribe subscribe)
        {
            var result = await _repository.Add(subscribe);
            //
            var created = _context.Subscribes.First(e => e.Email == subscribe.Email)
                .AsSource().OfLikeness<Subscribe>();
            Assert.True(created.Equals(subscribe));
            Assert.True(result.Equals(subscribe.Id));
        }

        [Theory, AutoMoqData]
        public async Task Add_Already_Exists(Subscribe subscribe)
        {
            subscribe.IsActive = false;
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            //
            var result = await _repository.Add(subscribe);
            //
            var count = _context.Subscribes.Count(e => e.Email == subscribe.Email);
            Assert.True(count == 1);
            Assert.True(subscribe.IsActive);
            Assert.True(result == subscribe.Id);
        }

        [Theory, AutoMoqData]
        public async Task Get_Success(Subscribe subscribe)
        {
            subscribe.IsActive = true;
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            //
            var result = await _repository.Get();
            //
            var source = result.First().AsSource().OfLikeness<Subscribe>();
            Assert.True(source.Equals(subscribe));
        }
    }
}