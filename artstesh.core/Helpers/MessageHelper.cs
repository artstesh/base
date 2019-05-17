using artstesh.core.RemoteAgents;

namespace artstesh.core.Helpers
{
    public class MessageHelper : IMessageHelper
    {
        private readonly IMailAgent _mailAgent;

        public MessageHelper(IMailAgent mailAgent)
        {
            _mailAgent = mailAgent;
        }

        public void AlarmAboutArticle(string mailTo, string link, string name, string unsubHash)
        {
            var message = "Приветствую,<br/> на сайте artstesh.ru появилась новая статья" +
                          $" - '{name}', почитать можно <a href=\"http://artstesh.ru{link}\">тут</a>, приятного чтения!<br/>" +
                          "Если по каким-то причинам вы хотите отписаться от рассылки с моего сайта, то можете" +
                          $" просто перейти по <a href=\"http://artstesh.ru/Sub/Unsub/{unsubHash}\">ссылке</a>. ";
            _mailAgent.SendMail(mailTo, "Новая статья на artstesh.ru!", message);
        }
    }
}