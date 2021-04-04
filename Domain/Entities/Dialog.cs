using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public class Dialog
    {
        public Dialog()
        {
            Messages = new HashSet<Message>();
            UserMembers = new List<CustomUser>();
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public List<CustomUser> UserMembers { get; set; }
        [Required]
        public IEnumerable<Message> Messages { get; set; }
    }
}
