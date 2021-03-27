using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skillsweb.Services;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class ProfileController: Controller
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProfileController(DataManager dataManager, IWebHostEnvironment hostingEnvironment, UserManager<CustomUser> userManager)
        {
            _dataManager = dataManager;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public IActionResult Show(string id)
        {
            id ??= _userManager.GetUserId(User);
            return View(_dataManager.Profiles.GetProfileByUserId(id));
        }

        public IActionResult Edit()
        {
            var guid = _userManager.GetUserId(User);
            UserProfile entity;
            entity = _dataManager.Profiles.GetProfileByUserId(guid);
            
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(UserProfile model, IFormFile avatarImageFile)
        {
            if (ModelState.IsValid)
            {
                if (avatarImageFile != null)
                {
                    model.AvatarImagePath = avatarImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, "images/", avatarImageFile.FileName),
                        FileMode.Create))
                    {
                        avatarImageFile.CopyTo(stream);
                    }
                }
                _dataManager.Profiles.SaveProfile(model);
                return RedirectToAction(nameof(Show));
            }
            return View(model);
        }
    }
}
