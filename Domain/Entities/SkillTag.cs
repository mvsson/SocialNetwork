using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class SkillTag : EntityBase
    {
        [Required]
        [Display(Name = "Название навыка")]
        public override string Name { get; set; }

        [Display(Name = "Описание навыка")] 
        public string Description { get; set; }

        [Display(Name = "Мастера")]
        public ICollection<UserProfile> MentorUsers { get; set; } = new List<UserProfile>();
        [Display(Name = "Ученики")]
        public ICollection<UserProfile> StudyUsers { get; set; } = new List<UserProfile>();
    }
}
