using System.Threading.Tasks;
using artstesh.data.Models;

namespace artstesh.data.Services
{
    public interface IFeedbackService
    {
        Task<bool> Add(FeedbackModel model);
    }
}