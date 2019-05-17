using System.Threading.Tasks;

namespace artstesh.data.Services
{
    public interface IAlarmService
    {
        Task AlarmAboutArticle(string name, string link);
    }
}