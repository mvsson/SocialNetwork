using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Models
{
    public class SearchViewModel
    {
        public string Request { get; set; }
        public IQueryable<SkillTag> Skills { get; set; }
        public IQueryable<UserProfile> Profiles { get; set; }
    }
}
