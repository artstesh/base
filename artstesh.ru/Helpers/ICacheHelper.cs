using System.Threading.Tasks;

namespace artstesh.ru.Helpers
{
    public interface ICacheHelper
    {
        Task<byte[]> Get(string key);
        Task Set(string key, object obj, int cacheLifeTime = -1);
        Task Remove(string key);
    }
}