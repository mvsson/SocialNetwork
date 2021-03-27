using System;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IUserTagsRepository
    {
        IQueryable<UserTag> GetUserTags();
        UserTag GetUserTagById(string id);
        UserTag GetUserTagByName(string codeWord);
        void SaveUserTag(UserTag entity);
        void DeleteUserTag(string id);
    }
}
