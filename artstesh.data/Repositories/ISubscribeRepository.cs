using System.Collections.Generic;
using System.Threading.Tasks;
using artstesh.data.Entities;

namespace artstesh.data.Repositories
{
    public interface ISubscribeRepository
    {
        Task<int> Add(Subscribe subscribe);
        Task<List<Subscribe>> Get();
        Task<bool> Unsubscribe(string secret);
    }
}