using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMS_ASP_MVC.Models;

namespace WMS_ASP_MVC.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserMgr {get;}
        private SignInManager<AppUser> SignInMgr { get; }

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUser user,string pwd)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FristName = user.FristName,
                    LastName = user.LastName,
                    companyID = user.companyID
                };

                if (pwd != null)
                {
                    IdentityResult result = await UserMgr.CreateAsync(appUser, pwd);
                    if (result.Succeeded)
                        return RedirectToAction("Index","Home");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
                else
                    ViewBag.message = "Password Cannot be NULL!!!!!!";
                return View();
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string pwd,string usr)
        {
            if (usr != null)
            {


                var result = await SignInMgr.PasswordSignInAsync(usr, pwd, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Result = "Password or Username is not correct";
                }
                return View();
            }
       
            else
            {
                ViewBag.Result= "Password or Username is not correct";
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}