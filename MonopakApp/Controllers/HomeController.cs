using MonopakApp.Models;
using MonopakApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MonopakApp.Controllers
{
    public class HomeController : Controller
    {
        MonoDB _context;
        public HomeController()
        {
            _context = new MonoDB();
        }
        public ActionResult Index()
        {
            Random rn = new Random();
            var productList = new List<Product>();
            var myProList = _context.Products.ToList();
            while (productList.Count < 12)
            {
                Product u = myProList[rn.Next(myProList.Count)];
                if (!productList.Contains(u))
                {
                    productList.Add(u);
                }
            }
            var vm = new HomeVm
            {
                Settings = _context.Settings.First(),
                TopSlider = _context.TopSliders.ToList(),
                about = _context.AboutUs.Find(1),
                ProductList = productList,
                CategoryList = _context.Categories.ToList(),
            };
            return View(vm);
        }
        public ActionResult ProList(int? catId)
        {
            if (catId == null) return HttpNotFound();
            ViewBag.prCount = _context.Products.Where(pr => pr.CategoryId == catId).Count();

            var prList = _context.Products.Where(pr =>  pr.CategoryId == catId).OrderBy(pr => pr.Id).ToList();

            var vm = new HomeVm
            {
                Settings = _context.Settings.First(),
                ProductList = prList
            };
            return PartialView("_ProductPartial", vm);
        }

        public ActionResult ProductList(int? id)
        {
            ViewBag.prCount = _context.Products.Where(pr => pr.CategoryId == id).Count();
            
            List<Product> proList = null ;
            if (id != null)
            {
                ViewBag.ProCatId = Url.RequestContext.RouteData.Values["id"];
                proList = _context.Products.Where(pr=>pr.CategoryId==id).OrderBy(pr => pr.Id).Take(9).ToList();
            }
            else
                proList = _context.Products.OrderBy(pr => pr.Id).Take(14).ToList();

            var vm = new HomeVm
            {
                ProductList = proList,
                Settings = _context.Settings.First()

            };
            return View(vm);
        }
        public ActionResult GetMorePro(int? catId,int skip,int take)
        {

            ViewBag.prCount = _context.Products.Where(pr => pr.CategoryId == catId).Count();

            var prList = _context.Products.Where(pr => pr.CategoryId == catId).OrderBy(pr => pr.Id).Skip(skip).Take(take).ToList();

            var vm = new HomeVm
            {
                Settings = _context.Settings.First(),
                ProductList = prList
            };
            return PartialView("_ProductPartial", vm);

        }


        public ActionResult Contact()
        {
            var vm = new HomeVm
            {
                Settings = _context.Settings.First()
            };
            return View(vm);
        }
    }
}