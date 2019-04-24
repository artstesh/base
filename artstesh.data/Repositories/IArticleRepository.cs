using System.Collections.Generic;
using System.Threading.Tasks;
using artstesh.data.Entities;

namespace artstesh.data.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> Get();
        Task<int> Create(Article article);
        Task<bool> Update(Article article);
        Task<bool> Delete(int id);
        Task<Article> Get(int id);
    }
}