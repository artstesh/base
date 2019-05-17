using System.Net;
using System.Net.Mail;
using artstesh.core.Config;

namespace artstesh.core.RemoteAgents
{
    public class MailAgent : IMailAgent
    {
        private readonly IConfigSettings _settings;

        public MailAgent(IConfigSettings settings)
        {
            _settings = settings;
        }

        public string SendMail(string mailto, string caption, string message, string attachFile = null)
        {
            var from = _settings.ApplicationKeys.MailFrom;
            var smtpServer = _settings.ApplicationKeys.MailServer;
            var pass = _settings.ApplicationKeys.MailPass;
            var mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(mailto));
            mail.Subject = caption;
            mail.Body = message;
            mail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(attachFile))
            {
                if (attachFile.Contains('^'))
                {
                    var attaches = attachFile.Split('^');
                    foreach (var attach in attaches) mail.Attachments.Add(new Attachment(attach));
                }
                else
                {
                    mail.Attachments.Add(new Attachment(attachFile));
                }
            }

            var client = new SmtpClient();
            client.Host = smtpServer;
            client.Port = 25;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, pass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            mail.Dispose();
            return "OK";
        }
    }
}