using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    public class AdminAccountController : Controller
    {
        MonoDB _context = new MonoDB();
        // GET: monopakadmin/AdminAccount
       public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            Setting selectAdmin = _context.Settings.Find(1);
            if (selectAdmin.AdminEmail == email && selectAdmin.AdminPassword == password)
            {
                Session["adminLogged"] = selectAdmin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Email və ya Parol səhvdir!";
            }

            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["adminLogged"] = null;

            return RedirectToAction("Login", "AdminAccount");
        }
    }
}