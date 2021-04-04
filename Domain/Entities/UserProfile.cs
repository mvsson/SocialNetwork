using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace SocialNetwork.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public string CustomUserId { get; set; }
        [ForeignKey(nameof(CustomUserId))]
        public CustomUser CustomUserOf { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string SurName { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [InverseProperty(nameof(SkillTag.MentorUsers))]
        [Display(Name = "Умею, практикую:")] 
        public ICollection<SkillTag> OwnSkills { get; set; } = new List<SkillTag>();

        [InverseProperty(nameof(SkillTag.StudyUsers))]

        [Display(Name = "Хочу научиться:")]
        public ICollection<SkillTag> WantedSkills { get; set; } = new List<SkillTag>();

        [Display(Name = "Фото ")]
        [DataType(DataType.ImageUrl)]
        public string AvatarImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
