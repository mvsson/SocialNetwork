using System;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IProfilesRepository
    {
        IQueryable<UserProfile> GetProfiles();
        UserProfile GetProfileByUserId(string guid);
        void SaveProfile(UserProfile entity);
    }
}
