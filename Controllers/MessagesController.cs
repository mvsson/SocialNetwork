using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly DataManager _dataManager;
        public MessagesController(UserManager<CustomUser> userManager, DataManager dataManager)
        {
            _userManager = userManager;
            _dataManager = dataManager;
        }
        public IActionResult Index()
        {
            var currentId = _userManager.GetUserId(User);
            var dialogs = _dataManager.Dialogs.GetDialogsByCurrentUserId(currentId);
            return View(dialogs);
        }

        public async Task<IActionResult> ShowDialogByUserId(string guid)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var selectedUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == guid);
            var dialog = _dataManager.Dialogs.GetDialogByMembers(currentUser, selectedUser);
            return View("ShowDialog", dialog);
        }

        public IActionResult ShowDialogById(string guid)
        {
            var dialog = _dataManager.Dialogs.GetDialogById(guid);
            return View("ShowDialog", dialog);
        }


        public async Task<IActionResult> Create(Message message, string dialogId)
        {
            if (ModelState.IsValid)
            {
                if (message.Text == null || message.Text?.Trim() == "")
                {
                    return Ok();
                }
                message.Name = User.Identity?.Name;
                message.DialogId = dialogId;
                await _dataManager.Messages.AddMessageAsync(message);
                return Ok();
            }
            return Error();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            throw new NotImplementedException();
        }
    }
}
