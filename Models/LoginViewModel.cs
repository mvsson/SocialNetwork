using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан email")]
        [Display(Name ="Электронная почта")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [UIHint("password")]
        [Display(Name ="Пароль")]
        public string Password { get; set; }

        [Display(Name ="Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
