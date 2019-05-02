using artstesh.data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace artstesh.ru.Models
{
    public class SubViewModel
    {
        public SubscribeModel SubscribeModel { get; set; }
        public string GoogleKey { get; set; }
    }
}