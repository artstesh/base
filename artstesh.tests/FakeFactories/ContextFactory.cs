using artstesh.data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace artstesh.tests.FakeFactories
{
    public static class ContextFactory
    {
        private static DbContextOptions<DataContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase("db", new InMemoryDatabaseRoot())
                .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }

        public static DataContext GetContext()
        {
            return new DataContext(CreateNewContextOptions());
        }
    }
}