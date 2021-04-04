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
        public ISkillTagsRepository SkillTags { get; set; }
        public IMessagesRepository Messages { get; set; }
        public IDialogsRepository Dialogs { get; set; }
        public DataManager(IProfilesRepository profiles, ISkillTagsRepository skillTags, IMessagesRepository messages, IDialogsRepository dialogs)
        {
            Profiles = profiles;
            SkillTags = skillTags;
            Messages = messages;
            Dialogs = dialogs;
        }
    }
}
