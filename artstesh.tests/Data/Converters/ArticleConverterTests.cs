using artstesh.data.Converters;
using artstesh.data.Entities;
using artstesh.data.Helpers;
using artstesh.data.Models;
using artstesh.tests.FakeFactories;
using SemanticComparison.Fluent;
using Xunit;

namespace artstesh.tests.Data.Converters
{
    public class ArticleConverterTests
    {
        [Theory, AutoMoqData]
        public void ToModel_Success(Article article)
        {
            var text = article.Text;
            article.Text = StringCompressor.CompressString(article.Text);
            //
            var expected = new ArticleModel
            {
                Created = article.Created, Id = article.Id, Text = text, Preview = article.Preview, Title = article.Title
            };
            //
            var model = article.ToModel().AsSource().OfLikeness<ArticleModel>();
            //
            Assert.True(model.Equals(expected));
        }
        
        [Fact, AutoMoqData]
        public void ToModel_Null()
        {
            var result = ((Article) null).ToModel();
            Assert.Null(result);
        }
        
        [Theory, AutoMoqData]
        public void FromModel_Success(ArticleModel article)
        {
            var expected = new Article
            {
                Created = article.Created, Id = article.Id, Text = StringCompressor.CompressString(article.Text), Preview = article.Preview, Title = article.Title
            };
            //
            var model = article.FromModel().AsSource().OfLikeness<Article>();
            //
            Assert.True(model.Equals(expected));
        }
        
        [Fact, AutoMoqData]
        public void FromModel_Null()
        {
            var result = ((ArticleModel) null).FromModel();
            Assert.Null(result);
        }
    }
}