using DemoApp.DataAccess.Entities;
using DemoApp.MVC.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoApp.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [Dependency]
        public UserModel UserModel { get; set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var db = GetDataBaseName(model.Email);
                var user = UserModel.Init(db).Validate(model.Email, model.Password); 
                if ( user != null)
                {
                    System.Web.HttpContext.Current.Session.Add("UserId", user.Id);
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The email or password provided is incorrect.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = GetDataBaseName(model.Email);
                var newUser = new User
                {
                    Email = model.Email,
                    Password = model.Password
                };
                UserModel.Init(db);
                User user = UserModel.Register(newUser);
                if (user != null)
                {
                    System.Web.HttpContext.Current.Session.Add("UserId", user.Id);
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Registration is not successful");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}