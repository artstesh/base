namespace artstesh.core.Helpers
{
    public interface IMessageHelper
    {
        void AlarmAboutArticle(string mailTo, string link, string name, string unsubHash);
    }
}