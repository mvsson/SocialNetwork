using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Domain.Entities
{
    public class CustomUser : IdentityUser
    {
        public CustomUser() :base()
        {
            Dialogs = new List<Dialog>();
        }

        public UserProfile UserProfile { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }
    }
}
