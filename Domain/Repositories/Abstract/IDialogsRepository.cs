using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IDialogsRepository
    {
        IQueryable<Dialog> GetDialogsByCurrentUserId(string guid);
        Dialog GetDialogById(string guid);
        Task<Dialog> GetDialogByMembers(CustomUser currentUser, CustomUser selectedUser);
    }
}
