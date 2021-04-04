using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Models;
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

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = _userManager.GetUserId(User);
                ViewBag.IsCurrentUser = true;
            }else if (id == _userManager.GetUserId(User))
            {
                ViewBag.IsCurrentUser = true;
            }
            else
            {
                ViewBag.IsCurrentUser = false;
            }
            return View(_dataManager.Profiles.GetProfileByUserId(id));
        }

        public IActionResult Edit()
        {
            var guid = _userManager.GetUserId(User);
            UserProfile profile = _dataManager.Profiles.GetProfileByUserId(guid);
            var skillTags = _dataManager.SkillTags.GetAllSkills();
            var model = new EditProfileViewModel()
            {
                Profile = profile,
                SkillTags = skillTags
            }; 
            
            return View(model);
        }
        [RequestSizeLimit(3*1024*1024)]
        [HttpPost]
        public IActionResult Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PhotoFile != null)
                {
                    model.Profile.AvatarImagePath = model.PhotoFile.FileName;
#pragma warning disable IDE0063 
                    using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, "images/", model.PhotoFile.FileName),
#pragma warning restore IDE0063 
                        FileMode.Create))
                    {
                        model.PhotoFile.CopyTo(stream);
                    }
                }
                _dataManager.Profiles.SaveProfile(model.Profile);
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        public IActionResult AddSkill(string name, string codeWord) // "codeWord" is 'want' or 'own', that arg is for identity skills collection
        {
            if (ModelState.IsValid && name != null && name.Trim() != "")
            {
                var profile = _dataManager.Profiles.GetProfileByUserId(_userManager.GetUserId(User));
                var skill = _dataManager.SkillTags.GetSkillByNameOrNew(name.Trim());
                switch (codeWord)
                {
                    case "own":
                        _dataManager.Profiles.AddOwnSkillInProfile(profile, skill);
                        break;
                    case "want":
                        _dataManager.Profiles.AddWantSkillInProfile(profile, skill);
                        break;
                }
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        public IActionResult DeleteSkill(string skillId, string codeWord)
        {
            var profile = _dataManager.Profiles.GetProfileByUserId(_userManager.GetUserId(User));
            var skill = _dataManager.SkillTags.GetSkillById(skillId);

            switch (codeWord)
            {
                case "own":
                    _dataManager.Profiles.DeleteOwnTagFromProfile(profile, skill);
                    break;
                case "want":
                    _dataManager.Profiles.DeleteWantTagFromProfile(profile, skill);
                    break;
            }
            return RedirectToAction(nameof(Edit));
        }

        public IActionResult DeleteAvatar()
        {
            var id = _userManager.GetUserId(User);
            var profile = _dataManager.Profiles.GetProfileByUserId(id);
            profile.AvatarImagePath = null;
            _dataManager.Profiles.SaveProfile(profile);
            return RedirectToAction(nameof(Edit));
        }
    }
}
