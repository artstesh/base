namespace C2c.Config
{
    public class ApplicationKeys
    {
        public virtual int CacheLifeTime { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string GoogleKey { get; set; }
        public virtual string GoogleSecretKey { get; set; }
        public virtual string MailFrom { get; set; }
        public virtual string MailServer { get; set; }
        public virtual string MailPass { get; set; }
    }
}