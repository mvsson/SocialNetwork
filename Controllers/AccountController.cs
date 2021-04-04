using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return LocalRedirect(returnUrl??"/");
            }
            ViewBag.returlUrl = returnUrl;
            return View(nameof(Login));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                CustomUser customUser = await _userManager.FindByNameAsync(model.UserName);
                if (customUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(customUser, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            returnUrl ??= "/";
            if (ModelState.IsValid)
            {
                var user = new CustomUser()
                {
                    UserName = model.Email, 
                    Email = model.Email, 
                    UserProfile = new UserProfile() { Name = model.Email, Email = model.Email}
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
#pragma warning disable IDE0037
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
#pragma warning restore IDE0037
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
#pragma warning disable IDE0037
                        return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = returnUrl });
#pragma warning restore IDE0037
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
