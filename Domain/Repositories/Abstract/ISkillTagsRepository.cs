using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface ISkillTagsRepository
    {
        IQueryable<SkillTag> GetAllSkills();
        SkillTag GetSkillById(string guid);
        SkillTag GetSkillByNameOrNew(string codeWord);
        //void AddNewSkill(string entity);
        void DeleteSkill(string id);
    }
}
