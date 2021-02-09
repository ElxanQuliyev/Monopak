using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    public class MapsController : Controller
    {
        private readonly MonoDB _context=new MonoDB();

        // GET: monopakadmin/Maps
        public ActionResult Index()
        {
            ViewBag.Settings = _context.Settings.First();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string mapLocation)
        {
            _context.Settings.First().Location = mapLocation;
            _context.SaveChanges();
            ViewBag.Settings = _context.Settings.First();

            return RedirectToAction("index");
        }
    }
}