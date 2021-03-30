using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Areas.Identity.Account
{
    public class Login :Controller
    {
        [Route("Identity/Account/Login")]
        public IActionResult LoginRedirect(string ReturnUrl)
        {
            return Redirect("/Account/?ReturnUrl=" + ReturnUrl);
        }
    }
}
