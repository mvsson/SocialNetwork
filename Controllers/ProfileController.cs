using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;

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
            UserProfile entity = _dataManager.Profiles.GetProfileByUserId(guid);
            
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(UserProfile model, IFormFile avatarImageFile)
        {
            if (ModelState.IsValid && avatarImageFile.IsPicExtention() && avatarImageFile.IsValidSize(2*1024*1024)) // size = 2Mb
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
