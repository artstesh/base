using System;

namespace artstesh.data.Models
{
    [Serializable]
    public class ArticleModel : IEquatable<ArticleModel>
    {
        public bool Equals(ArticleModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Text, other.Text) && 
                   string.Equals(Preview, other.Preview) && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ArticleModel) obj);
        }

        public override int GetHashCode()
        {
             return Id;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}