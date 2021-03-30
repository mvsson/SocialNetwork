using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories.Abstract;

namespace SocialNetwork.Domain.Repositories.EntityFramework
{
    public class EfProfilesRepository : IProfilesRepository
    {
        private readonly ApplicationDbContext _context;
        public EfProfilesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserProfile> GetProfiles()
        {
            return _context.Profiles;
        }

        public UserProfile GetProfileByUserId(string guid)
        {
            if (_context.Profiles.Any(p => p.CustomUserId == guid))
                return _context.Profiles?.Where(p => p.CustomUserId == guid).FirstOrDefault();
            
            // дальнейшая часть метода на случай, если юзер был добавлен не через регистрацию на сайте //
            var user = _context.Users?.Where(u => (u.Id) == (guid)).FirstOrDefault(); 
            user.UserProfile ??= new UserProfile()
            {
                CustomUserId = user.Id,
                Email = user.Email,
            };
            return user.UserProfile;
        }

        public ICollection<UserProfile> GetProfilesByTag(string tag)
        {
            var tagWithProfiles = _context.UserTags.Where(t => t.Name == tag).Include(t => t.UserProfiles).FirstOrDefault();
            return tagWithProfiles?.UserProfiles;
        }

        public void SaveProfile(UserProfile entity)
        {
            _context.Entry(entity).State = _context.Profiles.Any(p => p.CustomUserId == entity.CustomUserId) ? EntityState.Modified : EntityState.Added;
            _context.SaveChanges();
        }
    }
}
