using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IProfilesRepository
    {
        IQueryable<UserProfile> GetProfiles();
        UserProfile GetProfileByUserId(string guid);
        IQueryable<UserProfile> GetProfilesBySkill(string tag);
        void SaveProfile(UserProfile entity);
        void AddOwnSkillInProfile(UserProfile profile, SkillTag tag);
        void AddWantSkillInProfile(UserProfile profile, SkillTag tag);
        void DeleteOwnTagFromProfile(UserProfile profile, SkillTag tag);
        void DeleteWantTagFromProfile(UserProfile profile, SkillTag tag);
    }
}
