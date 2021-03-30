using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IUserTagsRepository
    {
        IQueryable<UserTag> GetUserTags();
        ICollection<UserTag> GetTagsByUserId(string guid);
        UserTag GetUserTagByName(string codeWord);
        void SaveUserTag(UserTag entity);
        void DeleteUserTag(string id);
    }
}
