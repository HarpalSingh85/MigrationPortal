using MigrationPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MigrationPortal.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            else
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return this.Redirect(returnUrl);
                    }

                    string nics = Request.Form["NICAdapters"];
                    List<Adapter> NICAdapters = JsonConvert.DeserializeObject<List<Adapter>>(nics);

                    UserModel usrmodel = new UserModel();
                    usrmodel.Hostname = model.HostName;
                    usrmodel.UserID = model.UserName;
                    usrmodel.NetworkAdapters = new List<Adapter>();
                    usrmodel.NetworkAdapters.AddRange(NICAdapters);
                    TempData["UserDetails"] = usrmodel;
                    return this.RedirectToAction("UserForm", "Home");
                }

                this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");

                return this.View(model);

            }


        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");

        }
    }
}