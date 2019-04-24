

using System;
using System.ComponentModel.DataAnnotations.Schema;
using QuizWeb.Data.Entities;

namespace artstesh.data.Entities
{
    [Table("article")]
    public class Article : BaseEntity
    {
        public bool Equals(Article other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Text, other.Text) && string.Equals(Preview, other.Preview) && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Article) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        [Column("Text")]
        public string Text { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Preview")]
        public string Preview { get; set; }
        [Column("Created")]
        public DateTime Created { get; set; }
    }
}