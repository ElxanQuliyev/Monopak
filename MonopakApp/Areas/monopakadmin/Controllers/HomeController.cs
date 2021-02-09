using MonopakApp.Areas.monopakadmin.ViewModels;
using MonopakApp.Models;
using MonopakApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {
        private MonoDB _context = new MonoDB();
        // GET: monopakadmin/Home
        public ActionResult Index()
        {
            AdminHomeVm model = new AdminHomeVm();

            model.TotalOrder = OrdersService.Instance.SearchOrdersCount();
            model.CategoryCount = _context.Categories.Count();
            model.ProductCount = _context.Products.Count();
            //model.UserCount = Membership.GetAllUsers().Count;
            return View(model);
        }
    }
}