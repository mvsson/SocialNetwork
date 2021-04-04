using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories.Abstract;
using SocialNetwork.Services;

namespace SocialNetwork.Domain.Repositories.EntityFramework
{
    public class EfSkillTagsRepository : ISkillTagsRepository
    {
        private readonly ApplicationDbContext _context;
        public EfSkillTagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<SkillTag> GetAllSkills()
        {
            return _context.SkillTags;
        }

        public SkillTag GetSkillById(string guid)
        {
            return _context.SkillTags.FirstOrDefault(s => s.Id == guid);
        }

        public SkillTag GetSkillByNameOrNew(string name)
        {
            name = name.CapitalizeFirst();
            var tag = _context.SkillTags?.FirstOrDefault(x => x.Name == name);
            if (tag == null)
            {
                tag = new SkillTag() { Name = name };
                _context.SkillTags.Add(tag);
                _context.SaveChanges();
            }

            return tag;
        }

        public void DeleteSkill(string name)
        {
            _context.SkillTags.Remove(new SkillTag() { Name = name });
            _context.SaveChanges();
        }
    }
}
