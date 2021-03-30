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
        ICollection<UserProfile> GetProfilesByTag(string tag);
        void SaveProfile(UserProfile entity);
    }
}
