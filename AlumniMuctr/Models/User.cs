using AlumniMuctr.Enums;
using System.ComponentModel.DataAnnotations;

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

    public class NewUserViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Повторите пароль")]
        public string RepeatPassword { get; set; }
    }
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }
    }
}
