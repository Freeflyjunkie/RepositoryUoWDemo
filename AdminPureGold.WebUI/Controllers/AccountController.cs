using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.WebUI.ViewModels;
using System;

namespace AdminPureGold.WebUI.Controllers
{    
    public class AccountController : Controller
    {
        private readonly IWeichertCoreService _weichertCoreService;
        public AccountController(IWeichertCoreService weichertCoreService)
        {
            _weichertCoreService = weichertCoreService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {                
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _weichertCoreService.GetWeichertOneUserByCredentials(model.UserName, model.Password);
            if (user != null)
            {
                if (user.PersonNumber != 0)
                {
                    CreateTicket(user);

                    if (Url.IsLocalUrl(returnUrl) &&
                        returnUrl.Length > 1 &&
                        returnUrl.StartsWith("/") &&
                        !returnUrl.StartsWith("//") &&
                        !returnUrl.StartsWith("/\\") &&
                        !returnUrl.EndsWith("/Account"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("CustomError", "Invalid credentials");
            return View(model);       
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private void CreateTicket(WeichertOneUser user)
        {
            // Sets Context.User.Identity.Name
            var authTicket = new FormsAuthenticationTicket(1, // Version
                   user.PersonNumber.ToString(CultureInfo.InvariantCulture).Trim(), // Name
                   DateTime.Now,  // Issued
                   DateTime.Now.AddMinutes(30), // Expires
                   true, // Persist
                   user.PersonNumber.ToString(CultureInfo.InvariantCulture).Trim()); // UserData

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(cookie);            
        }
    }
}
