using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsersController : Controller
    {
        ApplicationDbContext context;

        public UsersController()
        {
            context = new ApplicationDbContext();
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Administrador")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }



        public ActionResult Index()
        {
            //if (User.Identity.loc)
                //{
                //    var user = User.Identity;
                //    ViewBag.Name = user.Name;
                //    //	ApplicationDbContext context = new ApplicationDbContext();
                //    //	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //    //var s=	UserManager.GetRoles(user.GetUserId());
                //    ViewBag.displayMenu = "No";

                //    if (isAdminUser())
                //    {
                //        ViewBag.displayMenu = "Yes";
                //    }
                //    return View();
                //}
                //else
                //{
                //    ViewBag.Name = "Not Logged IN";
                //}

                if (User.Identity.IsAuthenticated)
            {

                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            var Users = context.Users.ToList();
            return View(Users);

        }
    }
}