using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    public class UserTagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
