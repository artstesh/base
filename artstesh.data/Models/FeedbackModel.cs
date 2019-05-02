using System;
using System.ComponentModel.DataAnnotations;

namespace artstesh.data.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "А как я смогу ответить?")]
        [MaxLength(200, ErrorMessage = "У вас очень длинный e-mail, можно что-то меньше 200 букв?")]
        public string Email { get; set; }

        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Мне неудобно обращаться к тебе \"Эй, ты.\"!")]
        [MaxLength(200, ErrorMessage = "У вас очень длинное имя, можно что-то меньше 200 букв?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Краткость сестра таланта, но все же...")]
        [MaxLength(9000, ErrorMessage = "Я польщен, но можно ли уложиться в 9000 символов?")]
        public string Message { get; set; }
    }
}