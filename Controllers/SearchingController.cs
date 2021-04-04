using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class SearchingController : Controller
    {
        private readonly DataManager _dataManager;
        public SearchingController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index(SearchViewModel model)
        {
            model.Skills = _dataManager.SkillTags.GetAllSkills();
            model.Profiles = string.IsNullOrEmpty(model.Request) ?
                    _dataManager.Profiles.GetProfiles() :
                    _dataManager.Profiles.GetProfilesBySkill(model.Request);
            
            return View(model);
        }
        public IActionResult Search(string skill)
        {
            var model = new SearchViewModel
            {
                Skills = _dataManager.SkillTags.GetAllSkills(),
                Profiles = string.IsNullOrEmpty(skill)
                        ? _dataManager.Profiles.GetProfiles()
                        : _dataManager.Profiles.GetProfilesBySkill(skill)
            };

            return View("Index", model);
        }
    }
}
