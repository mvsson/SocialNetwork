using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class Message : EntityBase
    {
        public string Text { get; set; }

        [Display(Name = "Имя отправителя")]
        public override string Name { get; set; }

        public string DialogId { get; set; }
        [ForeignKey(nameof(DialogId))]
        public Dialog Dialog { get; set; }
    }
}
