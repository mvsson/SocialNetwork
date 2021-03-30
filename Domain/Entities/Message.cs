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

        public string SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public CustomUser Sender { get; set; }

        public string RecipientId { get; set; }
    }
}
