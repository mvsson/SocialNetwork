using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories.Abstract;

namespace SocialNetwork.Domain.Repositories.EntityFramework
{
    public class EfUserTagsRepository : IUserTagsRepository
    {
        private readonly ApplicationDbContext _context;
        public EfUserTagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserTag> GetUserTags()
        {
            return _context.UserTags;
        }

        public ICollection<UserTag> GetTagsByUserId(string guid)
        {
            var profileWithTags = _context.Profiles.Where(p => p.CustomUserId == guid).Include(p => p.UserTags).FirstOrDefault();
            return profileWithTags?.UserTags;
        }

        public UserTag GetUserTagByName(string name)
        {
            return _context.UserTags.FirstOrDefault(x => x.Name == name);
        }

        public void SaveUserTag(UserTag entity)
        {
            if (_context.Profiles.Any(t => t.Name == entity.Name))
                return;
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void DeleteUserTag(string name)
        {
            _context.UserTags.Remove(new UserTag() { Name = name });
            _context.SaveChanges();
        }
    }
}
