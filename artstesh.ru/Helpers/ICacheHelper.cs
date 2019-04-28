using System.Threading.Tasks;

namespace C2c.Helper
{
    public interface ICacheHelper
    {
        Task<byte[]> Get(string key);
        Task Set(string key, object obj, int cacheLifeTime =-1);
        Task Remove(string key);
    }
}