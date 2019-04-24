

namespace C2c.Config
{
    public class ApplicationKeys
    {
        public virtual int CacheLifeTime { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }

        public override bool Equals(object obj)
        {
            var keys = obj as ApplicationKeys;
            return keys != null &&
                   CacheLifeTime == keys.CacheLifeTime && Login == keys.Login && Password == keys.Password;
        }
    }
}