using System;

namespace artstesh.data.Models
{
    public class SubscribeModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime BeginDate { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
    }
}