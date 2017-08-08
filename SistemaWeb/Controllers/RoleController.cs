using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RoleController : Controller
    {
        ApplicationDbContext context;
        // GET: Role

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

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

            var Roles = context.Roles.ToList();
            return View(Roles);

        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
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
        
        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
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

            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
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

            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(context.Roles.Find(Id).ToString()))
            {
                return HttpNotFound();
            }
            return View(context.Roles.Find(Id));
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole Role)
        {
            if (ModelState.IsValid)
            {
                context.Entry(Role).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Role);
        }

        public ActionResult Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(context.Roles.Find(Id).ToString()))
            {
                return HttpNotFound();
            }
            return View(context.Roles.Find(Id));
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(IdentityRole Role)
        {
            //Employee employee = db.Employees.Find(id);
            if (Role != null)
            {
                IdentityRole role = context.Roles.Find(Role.Id);
                context.Roles.Remove(role);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}