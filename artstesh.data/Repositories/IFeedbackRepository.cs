using System.Threading.Tasks;
using artstesh.data.Entities;

namespace artstesh.data.Repositories
{
    public interface IFeedbackRepository
    {
        Task<bool> Add(Feedback feedback);
    }
}