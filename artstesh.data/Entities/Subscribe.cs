using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace artstesh.data.Entities
{
    [Table("subscribe")]
    public class Subscribe : BaseEntity
    {
        [Column("Email")] public string Email { get; set; }

        [Column("BeginDate")] public DateTime BeginDate { get; set; }

        [Column("IsActive")] public bool IsActive { get; set; }

        [Column("Name")] public string Name { get; set; }

        [Column("Secret")] public string Secret { get; set; }
    }
}