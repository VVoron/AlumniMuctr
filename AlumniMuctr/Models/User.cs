using AlumniMuctr.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AlumniMuctr.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
