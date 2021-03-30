using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class UserTag : EntityBase
    {
        [Display(Name = "Название навыка")]
        public override string Name { get; set; }

        [Display(Name = "Описание навыка")] 
        public string Description { get; set; }

        [Display(Name = "Пользователи")]
        public ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    }
}
