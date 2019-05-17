using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace artstesh.data.Entities
{
    [Table("feedback")]
    public class Feedback : BaseEntity
    {
        [Column("Email")] public string Email { get; set; }

        [Column("Created")] public DateTime Created { get; set; }

        [Column("Name")] public string Name { get; set; }

        [Column("Message")] public string Message { get; set; }
    }
}