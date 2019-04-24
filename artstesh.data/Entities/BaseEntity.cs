using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWeb.Data.Entities
{
    public abstract class BaseEntity
    {
        [Column("Id")] public int Id { get; set; }
    }
}