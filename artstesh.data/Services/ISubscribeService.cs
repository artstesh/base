using System.Collections.Generic;
using System.Threading.Tasks;
using artstesh.data.Models;

namespace artstesh.data.Services
{
    public interface ISubscribeService
    {
        Task<int> Add(SubscribeModel subscribe);
        Task<List<SubscribeModel>> Get();
        Task<bool> Unsubscribe(string secret);
    }
}