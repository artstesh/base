using System;
using System.ComponentModel.DataAnnotations;

namespace artstesh.data.Models
{
    public class SubscribeModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "А куда отпавлять новости?!")]
        [MaxLength(200, ErrorMessage = "У вас очень длинный e-mail, можно что-то меньше 200 букв?")]
        public string Email { get; set; }

        public DateTime BeginDate { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Мне неудобно обращаться к тебе \"Эй, ты.\"!")]
        [MaxLength(200, ErrorMessage = "У вас очень длинное имя, можно что-то меньше 200 букв?")]
        public string Name { get; set; }

        public string Secret { get; set; }
    }
}