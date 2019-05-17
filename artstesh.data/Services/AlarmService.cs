using System.Threading.Tasks;
using artstesh.core.Helpers;

namespace artstesh.data.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly IMessageHelper _messageHelper;
        private readonly ISubscribeService _subscribeService;

        public AlarmService(IMessageHelper messageHelper, ISubscribeService subscribeService)
        {
            _messageHelper = messageHelper;
            _subscribeService = subscribeService;
        }

        public async Task AlarmAboutArticle(string name, string link)
        {
            var subscribers = await _subscribeService.Get();
            for (var i = 0; i < subscribers.Count; i++)
                _messageHelper.AlarmAboutArticle(subscribers[i].Email, link, name, subscribers[i].Secret);
        }
    }
}