using artstesh.data.Entities;
using artstesh.data.Models;

namespace artstesh.data.Converters
{
    public static class SubscribeConverter
    {
        public static SubscribeModel ToModel(this Subscribe subscribe)
        {
            if (subscribe == null) return null;
            return new SubscribeModel
            {
                BeginDate = subscribe.BeginDate, Id = subscribe.Id, Email = subscribe.Email, Name = subscribe.Name, IsActive = subscribe.IsActive, Secret = subscribe.Secret
            };
        }
        public static Subscribe FromModel(this SubscribeModel subscribe)
        {
            if (subscribe == null) return null;
            return new Subscribe
            {
                BeginDate = subscribe.BeginDate, Id = subscribe.Id, Email = subscribe.Email, Name = subscribe.Name, IsActive = subscribe.IsActive, Secret = subscribe.Secret
            };
        }
    }
}