using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {
        // GET: monopakadmin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}