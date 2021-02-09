using MonopakApp.Models;
using MonopakApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Controllers
{
    public class WidgetsController : Controller
    {
        
        private MonoDB _context= new MonoDB();

        // GET: Widgets
        public ActionResult CategoryList()
        {
            BaseViewModel vm = new BaseViewModel()
            {
                CategoryList = _context.Categories.OrderByDescending(x => x.Id).ToList()
            };
            return PartialView("_CategoryList",vm);
        }
    }
}