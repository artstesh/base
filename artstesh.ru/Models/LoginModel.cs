using System.ComponentModel.DataAnnotations;

namespace artstesh.ru.Models
{
    public class LoginModel
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
        public string GoogleKey { get; set; }
    }
}