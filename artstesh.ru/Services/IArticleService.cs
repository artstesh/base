using System.Collections.Generic;
using System.Threading.Tasks;
using artstesh.data.Models;

namespace artstesh.ru.Services
{
    public interface IArticleService
    {
        Task<List<ArticleModel>> Get();
        Task<ArticleModel> GetCached(int id);
        Task<int> Create(ArticleModel article);
        Task<bool> Update(ArticleModel article);
        Task<bool> Delete(int id);
    }
}