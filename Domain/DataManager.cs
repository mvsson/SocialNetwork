using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Domain.Repositories.Abstract;

namespace SocialNetwork.Domain
{
    public class DataManager
    {
        public IProfilesRepository Profiles { get; set; }
        public IUserTagsRepository UserTags { get; set; }
        public DataManager(IProfilesRepository profiles, IUserTagsRepository userTags)
        {
            Profiles = profiles;
            UserTags = userTags;
        }
    }
}
