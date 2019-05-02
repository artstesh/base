using System;

namespace artstesh.data.Models
{
    [Serializable]
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}