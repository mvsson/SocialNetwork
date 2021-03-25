using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class Skill : EntityBase
    {
        [Display(Name = "Название навыка")]
        public override string Name { get; set; }

        [Display(Name = "Описание навыка")] 
        public override string Description { get; set; }
    }
}
