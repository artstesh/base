using artstesh.data.Models;
using artstesh.tests.FakeFactories;
using Xunit;

namespace artstesh.tests.Data.Models
{
    public class ArticleModelModelTests
    {
        [Theory, AutoMoqData]
        public void Equals_True(ArticleModel model)
        {
            var other = Clone(model);
            Assert.True(other.Equals(model));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Text(ArticleModel model, string text)
        {
            var other = Clone(model);
            other.Text = text;
            Assert.False(other.Equals(model));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Preview(ArticleModel model, string preview)
        {
            var other = Clone(model);
            other.Preview = preview;
            Assert.False(other.Equals(model));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Title(ArticleModel model, string title)
        {
            var other = Clone(model);
            other.Title = title;
            Assert.False(other.Equals(model));
        }

        private static ArticleModel Clone(ArticleModel model)
        {
            return new ArticleModel
            {
                Text = model.Text, Preview = model.Preview, Title = model.Title, Id = model.Id
            };
        }

        [Theory]
        [AutoMoqData]
        public void Equals_False_Null(ArticleModel origin)
        {
            Assert.False(origin.Equals(null));
        }

        [Theory]
        [AutoMoqData]
        public void Equals_True_Self(ArticleModel origin)
        {
            Assert.True(origin.Equals(origin));
        }

        [Theory]
        [AutoMoqData]
        public void Equals_False_Wrong_Type(ArticleModel origin)
        {
            Assert.False(origin.Equals(new object()));
        }
    }
}