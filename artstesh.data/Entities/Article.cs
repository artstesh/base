using System;
using System.ComponentModel.DataAnnotations.Schema;
using QuizWeb.Data.Entities;

namespace artstesh.data.Entities
{
    [Table("article")]
    public class Article : BaseEntity
    {
        [Column("Text")] public string Text { get; set; }

        [Column("Title")] public string Title { get; set; }

        [Column("Preview")] public string Preview { get; set; }

        [Column("Created")] public DateTime Created { get; set; }
    }
}