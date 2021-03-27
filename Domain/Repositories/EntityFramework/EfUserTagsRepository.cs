using System;
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

        public UserTag GetUserTagById(string id)
        {
            return _context.UserTags.FirstOrDefault(x => x.Id == id);
        }

        public UserTag GetUserTagByName(string name)
        {
            return _context.UserTags.FirstOrDefault(x => x.Name == name);
        }

        public void SaveUserTag(UserTag entity)
        {
            _context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUserTag(string id)
        {
            _context.UserTags.Remove(new UserTag() { Id = id });
            _context.SaveChanges();
        }
    }
}
