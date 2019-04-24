using System.Diagnostics;
using artstesh.data.Entities;
using artstesh.tests.FakeFactories;
using Xunit;

namespace artstesh.tests.Data.Entities
{
    public class ArticleTests
    {
        [Theory, AutoMoqData]
        public void Equals_True(Article article)
        {
            var other = Clone(article);
            Assert.True(other.Equals(article));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Text(Article article, string text)
        {
            var other = Clone(article);
            other.Text = text;
            Assert.False(other.Equals(article));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Preview(Article article, string preview)
        {
            var other = Clone(article);
            other.Preview = preview;
            Assert.False(other.Equals(article));
        }
        
        [Theory, AutoMoqData]
        public void Equals_Title(Article model, string title)
        {
            var other = Clone(model);
            other.Title = title;
            Assert.False(other.Equals(model));
        }

        private static Article Clone(Article article)
        {
            return new Article
            {
                Text = article.Text, Preview = article.Preview, Title = article.Title
            };
        }

        [Theory]
        [AutoMoqData]
        public void Equals_False_Null(Article origin)
        {
            Assert.False(origin.Equals(null));
        }

        [Theory]
        [AutoMoqData]
        public void Equals_True_Self(Article origin)
        {
            Assert.True(origin.Equals(origin));
        }

        [Theory]
        [AutoMoqData]
        public void Equals_False_Wrong_Type(Article origin)
        {
            Assert.False(origin.Equals(new object()));
        }
    }
}