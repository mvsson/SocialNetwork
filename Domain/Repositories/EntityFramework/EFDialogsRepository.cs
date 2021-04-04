using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories.Abstract;

namespace SocialNetwork.Domain.Repositories.EntityFramework
{
    public class EFDialogsRepository : IDialogsRepository
    {
        private readonly ApplicationDbContext _context;
        public EFDialogsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Dialog> GetDialogsByCurrentUserId(string guid)
        {
            return _context.Dialogs.Where(d => d.UserMembers.Any(user => user.Id == guid))
                        .Include(d => d.Messages)
                        .Include(d => d.UserMembers.Where(member => member.Id != guid))
                                .ThenInclude(user => user.UserProfile);
        }

        public Dialog GetDialogById(string guid)
        {
            return _context.Dialogs.Where(d => d.Id == guid)
                        .Include(d => d.Messages)
                        .Include(d => d.UserMembers)
                                .ThenInclude(user => user.UserProfile)
                        .FirstOrDefault();
        }

        public async Task<Dialog> GetDialogByMembers(CustomUser currentUser, CustomUser selectedUser)
        {
            var dialog = _context.Dialogs.Where(d => d.UserMembers.Count == 2 && 
                                                     d.UserMembers.All(user => user == currentUser || user == selectedUser))
                        .Include(d => d.Messages)
                        .Include(d => d.UserMembers)
                            .ThenInclude(user => user.UserProfile)
                        .FirstOrDefault();
            dialog ??= await GetNewDialogWithSelectUser(currentUser, selectedUser);
            return dialog;
        }

        private async Task<Dialog> GetNewDialogWithSelectUser(CustomUser currentUser, CustomUser selectedUser)
        {
            var dialog = new Dialog();
            dialog.UserMembers.Add(currentUser);
            dialog.UserMembers.Add(selectedUser);
            await _context.Dialogs.AddAsync(dialog);
            await _context.SaveChangesAsync();
            return dialog;
        }
    }
}
