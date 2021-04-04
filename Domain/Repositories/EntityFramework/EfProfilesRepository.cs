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
                return _context.Profiles?.Where(p => p.CustomUserId == guid)
                            .Include(p => p.OwnSkills)
                            .Include(p=> p.WantedSkills).FirstOrDefault();
            
            // дальнейшая часть метода на случай, если юзер был добавлен не через регистрацию на сайте //
            var user = _context.Users?.Where(u => (u.Id) == (guid)).FirstOrDefault(); 
            user.UserProfile ??= new UserProfile()
            {
                CustomUserId = user.Id,
                Email = user.Email,
            };
            return user.UserProfile;
        }

        public IQueryable<UserProfile> GetProfilesBySkill(string skill)
        {
            return _context.Profiles.Where(p =>
                p.OwnSkills.Any(sk => sk.Name == skill) ||
                p.WantedSkills.Any(sk => sk.Name == skill));
        }

        public void SaveProfile(UserProfile entity)
        {
            _context.Entry(entity).State = _context.Profiles.Any(p => p.CustomUserId == entity.CustomUserId) ? EntityState.Modified : EntityState.Added;
            _context.SaveChanges();
        }
        public void AddOwnSkillInProfile(UserProfile profile, SkillTag tag)
        {
            profile.OwnSkills.Add(tag);
            _context.SaveChanges();
        }

        public void AddWantSkillInProfile(UserProfile profile, SkillTag tag)
        {
            profile.WantedSkills.Add(tag);
            _context.Entry(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOwnTagFromProfile(UserProfile profile, SkillTag tag)
        {
            profile.OwnSkills.Remove(tag);
            _context.Entry(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteWantTagFromProfile(UserProfile profile, SkillTag tag)
        {
            profile.WantedSkills.Remove(tag);
            _context.Entry(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
