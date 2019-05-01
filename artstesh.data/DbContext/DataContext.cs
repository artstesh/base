using artstesh.data.Entities;
using Microsoft.EntityFrameworkCore;
using QuizWeb.Data.Converters;

namespace artstesh.data.DbContext
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            foreach (var property in entityType.GetProperties())
                if (property.ClrType == typeof(bool))
                    property.SetValueConverter(new BoolToIntConverter());
        }
    }
}