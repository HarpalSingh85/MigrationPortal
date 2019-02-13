using MigrationPortal.Helpers;
using MigrationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MigrationPortal.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        public ActionResult UserForm()
        {
            //ViewBag.Title = "Home Page";
            UserModel usrmodel = (UserModel)TempData["UserDetails"];
            ADHelper helper = new ADHelper();
            try
            {
                usrmodel = helper.GetUserDetails(usrmodel.UserID, usrmodel.Hostname, usrmodel.NetworkAdapters);                                 
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            
            return View(usrmodel);
        }

        [HttpPost]
        public ActionResult UserForm(UserModel usermodel)
        {
            var urmodel = usermodel;
            return View();
        }
    }
}
