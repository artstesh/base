using System.ComponentModel.DataAnnotations.Schema;

namespace artstesh.data.Entities
{
    public abstract class BaseEntity
    {
        [Column("Id")] public int Id { get; set; }
    }
}