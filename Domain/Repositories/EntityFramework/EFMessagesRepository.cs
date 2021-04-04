using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories.Abstract;

namespace SocialNetwork.Domain.Repositories.EntityFramework
{
    public class EFMessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _context;

        public EFMessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
