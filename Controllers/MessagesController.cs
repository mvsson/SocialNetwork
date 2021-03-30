using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            /*var currentUser = await _userManager.GetUserAsync(User);
            var currentProfile = _dataManager.Profiles.GetProfileByUserId(currentUser.Id);
            ViewBag.CurrentUserName = currentProfile.Name;*/
            var messages = _dataManager.Messages.GetAllMessages();
            //var messages = _dataManager.Messages.GetMessagesByUserId(currentUser.Id);
            return View(messages);
        }

        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.Name = User.Identity?.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.SenderId = sender.Id;
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
