using artstesh.data.Entities;
using artstesh.data.Helpers;
using artstesh.data.Models;

namespace artstesh.data.Converters
{
    public static class ArticleConverter
    {
        public static ArticleModel ToModel(this Article article)
        {
            if (article == null) return null;
            return new ArticleModel
            {
                Created = article.Created, Id = article.Id, Text = StringCompressor.DecompressString(article.Text),
                Preview = article.Preview, Title = article.Title, Published = article.Published
            };
        }

        public static Article FromModel(this ArticleModel article)
        {
            if (article == null) return null;
            return new Article
            {
                Created = article.Created, Id = article.Id, Text = StringCompressor.CompressString(article.Text),
                Preview = article.Preview, Title = article.Title, Published = article.Published
            };
        }
    }
}