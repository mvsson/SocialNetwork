using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SocialNetwork.Domain.Entities
{
    public class User : EntityBase
    {
        [Display(Name = "Имя")]
        public override string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string SurName { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 


        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Навыки")]
        public IEnumerable<Skill> Skills { get; set; }

        [Display(Name = "Фото")]
        public string AvatarImagePath { get; set; }
    }
}
