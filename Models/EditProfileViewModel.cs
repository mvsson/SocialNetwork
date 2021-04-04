using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;

namespace SocialNetwork.Models
{
    public class EditProfileViewModel
    {
        public UserProfile Profile { get; set; }

        [AllowedExtensions(new []{ ".jpeg", ".jpg", ".gif", ".png" })]
        public IFormFile PhotoFile { get; set; }
        public IQueryable<SkillTag> SkillTags { get; set; }
    }
}
