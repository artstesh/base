using System.Collections.Generic;
using System.Threading.Tasks;
using artstesh.data.Models;

namespace artstesh.data.Services
{
    public interface IArticleService
    {
        Task<List<ArticleModel>> Get();
        Task<int> Create(ArticleModel article);
        Task<bool> Update(ArticleModel article);
        Task<bool> Delete(int id);
        Task<ArticleModel> Get(int id);
    }
}