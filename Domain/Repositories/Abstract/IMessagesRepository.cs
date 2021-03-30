using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Repositories.Abstract
{
    public interface IMessagesRepository
    {
        IQueryable<Message> GetAllMessages();
        IQueryable<Message> GetMessagesByUserId(string guid);
        Task AddMessageAsync(Message message);


    }
}
