using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace C2c.Config
{
    public class ConfigSettings : IConfigSettings
    {
        private readonly IConfiguration _configuration;

        public ConfigSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("ConnectionStrings")["DataContext"];
            ApplicationKeys = SetApplicationKeys();
        }

        public string ConnectionString { get; set; }
        public ApplicationKeys ApplicationKeys { get; set; }

        public bool CheckPassword(string login, string password)
        {
            return login.Equals(ApplicationKeys.Login) && password.Equals(ApplicationKeys.Password);
        }

        private ApplicationKeys SetApplicationKeys()
        {
            var keys = new ApplicationKeys();
            var temp = _configuration.GetSection("appKeys").GetChildren().ToList();
            keys.CacheLifeTime = Convert.ToInt32(temp.FirstOrDefault(e => e.Key == "CacheLifeTime")?.Value ?? "500");
            keys.Login = temp.FirstOrDefault(e => e.Key == "Login")?.Value;
            keys.Password = temp.FirstOrDefault(e => e.Key == "Password")?.Value;
            keys.GoogleKey = temp.FirstOrDefault(e => e.Key == "GoogleKey")?.Value;
            keys.GoogleSecretKey = temp.FirstOrDefault(e => e.Key == "GoogleSecretKey")?.Value;
            keys.MailFrom = temp.FirstOrDefault(e => e.Key == "MailFrom")?.Value;
            keys.MailPass = temp.FirstOrDefault(e => e.Key == "MailPass")?.Value;
            keys.MailServer = temp.FirstOrDefault(e => e.Key == "MailServer")?.Value;
            return keys;
        }
    }
}